using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoItemsService
    {
        private ITodoRepository _todoRepository;

        public TodoItemsService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public List<TodoDTO> ListTodoItems()
        {
            return _todoRepository.GetAll()
                .Select(x => ToDTO(x))
                .ToList();
        }

        public TodoDTO? FindTodoItem(long id)
        {
            var todoItem = _todoRepository.GetById(id);

            if (todoItem == null) {
                throw new Exception("Not found");
            }

            return ToDTO(todoItem);
        }

        public void UpdateTodoItem(long id, TodoDTO todoDTO)
        {
            try
            {
                _todoRepository.Update(id, ToEntity(todoDTO));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public TodoDTO CreateTodoItem(TodoDTO todoDTO)
        {
            var todoItem = _todoRepository.Insert(new TodoItem {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name
            });

            return ToDTO(todoItem);
        }

        public bool? DeleteTodoItem(long id)
        {
            var deleted =  _todoRepository.Delete(id);

            if (!deleted) {
                throw new Exception("Not found");
            }

            return true;
        }

        private static TodoDTO ToDTO(TodoItem todoItem)
        {
            return new TodoDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        }
        private static TodoItem ToEntity(TodoDTO todoItemDTO)
        {
            return new TodoItem
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };
        }
    }
}
