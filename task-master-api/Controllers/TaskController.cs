using Microsoft.AspNetCore.Mvc;
using RmqLib.@interface;
using task_master_api.Exchanges;
using task_master_api.Models.Task;
using task_master_api.Queues;

namespace task_master_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IQueuePublisher<TaskQueue, DefaultExchange> _queuePublisher;

        public TaskController(IQueuePublisher<TaskQueue, DefaultExchange> queuePublisher)
        {
            _queuePublisher = queuePublisher;
        }

        [HttpGet]
        public IActionResult Get()
        {
            this._queuePublisher.Publish(new NewTaskModel
            {
                Title = "Test Task",
                Description = "This is a test task",
                Status = task_master_domain.task.TaskStatus.InProgress,
                Priority = task_master_domain.task.Priority.Medium,
                DueDate = DateTime.Now,
            });
            return Ok();
        }
    }
}