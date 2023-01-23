# Domain Models

## Task

```csharp

class Task
{
    Task Create(CreateTask createTask);
    Task GetTask(string taskId);
    void Update(UpdateTask updateTask);
    void Delete(string taskId);
    void AddSubtask(string taskId, string subTaskId);
    void RemoveSubTask(string taskId, string subTaskId);
    void AddComment(string taskId, string commentId);
    void RemoveComment(string taskId, string commentId);
    void AddTag(string taskId, Tag tag);
    void RemoveTag(string taskId, Tag tag);
}

``` 


```json
{
  "id": "task_id",
  "title": "This is a task title",
  "description": "This is a task description",
  "due_date": "2018-01-01",
  "priority": "high",
  "status": "open",
  "assignee": "user_id",
  "project": "project_id",
  "tags": ["tag1", "tag2"],
  "subtasks": ["subtask_id1", "subtask_id2"],
  "comments": ["comment_id1", "comment_id2"]
}
``` 