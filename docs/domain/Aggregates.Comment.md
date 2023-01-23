# Domain Models

## Comment

```csharp
class Comment
{
    Comment Create(CreateComment createComment);
    Comment GetComment(string commentId);
    void Reply(string commentId, string replyId);
    void Update(UpdateComment updateComment);
    void Delete(string commentId);
}
```


```json
{
  "id": "comment_id",
  "body": "comment body",
  "created_at": "2016-01-01T00:00:00.000Z",
  "reply_to": "comment_id",
  "user": "user_id"

}
```