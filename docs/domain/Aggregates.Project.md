# Domain Models

## Project

```csharp
class Project
{
    Project Create(CreateProject createProject);
    Project GetProject(string projectId);
    void Update(UpdateProject updateProject);
    void Delete(string projectId);
    void AddMember(string projectId, string userId);
    void RemoveMember(string projectId, string userId);
}
```


```json
{
  "id": "project_id",
  "name": "project_name",
  "description": "project_description",
  "created_at": "2015-01-01T00:00:00.000Z",
  "updated_at": "2015-01-01T00:00:00.000Z",
  "creator_id": "user_id",
  "organization_id": "organization_id",
  "members": ["user_id", "user_id"]
}

```