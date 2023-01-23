# Domain Models

## Organization

```csharp
class Organization
{
    Organization Create(CreateOrganization createOrganization);
    Organization GetOrganization(string organizationId);
    void Update(UpdateOrganization updateOrganization);
    void Delete(string organizationId);
    void AddMember(string organizationId, string userId);
    void RemoveMember(string organizationId, string userId);
    void AddAdministrator(string organizationId, string userId);
    void RemoveAdministrator(string organizationId, string userId);
}

```


```json
  {
    "id": "organization_id",
    "name": "organization_name",
    "description": "organization_description",
    "created_at": "2015-01-01T00:00:00.000Z",
    "updated_at": "2015-01-01T00:00:00.000Z",
    "creator_id": "user_id",
    "avatar_url": "https://example.com/avatar.png",
    "url": "https://example.com/organization",
    "administrators": ["user_id", "user_id"],
    "members": ["user_id", "user_id"]
  }

```