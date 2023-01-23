# Domain Models

## User

```csharp
class User
{
    User Create(CreateUser createUser);
    User GetUser(string userId);
    void Update(UpdateUser updateUser);
    void Delete(string userId);
}
```


```json
{
  "id": "user_id",
  "name": "user_name",
  "email": "user_email",
  "avatar": "user_avatar",
  "avatar_url": "user_avatar_url"
}
```