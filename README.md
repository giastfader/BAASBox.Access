# BAASBox.Access
A PCL library for accessing various features of the BAASBox web service API.

## Notes
These instructions presuppose that you have an instance of BAASBox already running.

BAASBox.Access has a dependency on the Flurl library for http/s communications.

*It's beyond the scope of the instructions to guide you through securing BAASBox if reaching it over the internet, but a good start would be to stand up an https proxy (such as NGINX), give it an SSL certificate, and lock down your BAASBox VM to only receive data on the BAASBox port from the proxy.*

## Getting started with authorisation

To use the library, you'll need a `BAASBoxConfig` object and an `AuthState` object to manage the endpoint and current user's authorisation state.

Create a BAASBoxConfig class and provide it with an endpoint for your solution, port, and app id:

```csharp
var config = new BAASBoxConfig("http://baasbox.example.com", 80, "1234567890");
```

Create an AuthState class:

```csharp
var auth = new AuthState();
```

To monitor changes to the user's authorisation state, register a `delegate Action<bool>` with the `OnAuthChange` event. This is deliberately as simple as possible: the bool indicates if the user is authorised or not.

```csharp
auth.OnAuthChange += UpdateUiBecauseAuthChanged;
```

Now you can make use of the `AuthLogic` class to perform a sign in...

```csharp
string username = ...;
string password = ...;

using (var logic = new AuthLogic(auth, config) {
  var result = await logic.SignInAsync(username, password);
  ...
}
```

NB. the `SignOut` method is not async - the sign out action for `AuthLogic` is to forget the Session Id.

## Getting started with social interactions

`AuthLogic` and `FeedLogic` both inherit from `BaseLogic` and provide some simple methods for accessing the social functions of BAASBox...

```csharp
using (var logic = new FeedLogic(auth, config) {
  var followResult = await FollowAsync(targetUsername);
  var unfollowResult = await UnfollowAsync(targetUsername);
  var following = await GetFollowingAsync();
  var followers = await GetFollowersAsync();
}
```

As per the BAASBox documentation, following a user will put you in the followers_of_**username** role. When the user posts a document, they may choose to **share** that document with that role (or any other).

## Documents

Documents are records created by users in BAASBox. By default can be stored and retrieved by their creator, but they can also be shared with different roles (including the convention followers_of_**username** role).

To manage documents of a given type, first create the appropriate collection inside BAASBox. Then extend `AbstractCrudObject` to represent your document type, and extend the `AbstractDocumentDAO<T>` to manage the CRUD and share operations:

```csharp
[DataContract]
public class SomeKindOfEntry : AbstractCrudObject 
{
  [DataMember]
  public DateTime Date { get; set; }

  [DataMember]
  public string FieldOne { get; set; }
  
  [DataMember]
  public string FieldTwo { get; set; }
}
```

`AbstractCrudObject` inherits from `BBData`, and provides some default fields: `id`, `_creation_date`, `_author`

The new DAO will need to know both the C# type of your new object (provided in the generic field), and the name of the collection it is stored in (provided to the base constructor):

```csharp
public class SomeKindOfEntryDAO : AbstractDocumentDAO<SomeKindOfEntry>
{
  public SomeKindOfEntryDAO(BAASBoxConfig config) : base(config, "userEntries")
  {
  }

  ...
}
```

You can then use this DAO to perform your CRUD and sharing operations on this new document type:

```csharp
SomeKindOfEntry doc = ...;

var sessionId = auth.SessionId;

using (var dao = new SomeKindOfEntryDAO(config)) {
  
  var createResult = await dao.CreateAsync(doc, sessionId); // creates a new document in BAASBox
  var updateResult = await dao.UpdateAsync(doc, sessionId); // updates the existing document in BAASBox
  
  // determines what to do based on whether or not the object's id field is currently null
  var submitResult = await dao.CreateOrUpdateAsync(doc, sessionId); 
  
  var getResult = await dao.GetAsync(id, sessionId); // retrieves an object by id
  var ListResults = await dao.ListAsync(sessionId); // retrieves all objects of this DAO's type
  
  var where = "FieldOne='Something'";
  var filteredResults = await dao.ListAsync(sessionId, where); // retrives all objects filtered by the where clause
  var recentResults = await dao.ListRecentAsync(10, sessionId, 0); // retrieves the most recent 10 objects
  var lessRecentResults = await dao.ListRecentAsync(10, sessionId, 1); // retrieves the 11th-20th recent objects
  
  // shares the object with the given id with the followers_of_**username** role
  var shareResult = await dao.ShareAsync(id, username, sessionId);
  
  // unshares the object with the given id from the followers_of_**username** role
  var unshareResult = await dao.UnshareAsync(id, username, sessionId);
  
  // NB. to share/unshare with other roles, provide the role name as an additional optional parameter
  
  var deleteResult = await dao.DeleteAsync(doc); // deletes the given document
}
```
