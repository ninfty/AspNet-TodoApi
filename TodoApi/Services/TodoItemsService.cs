using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoItemsService
    {
        private TodoContext _context;

        public TodoItemsService(TodoContext context)
        {
            _context = context;
        }

        public List<TodoItemDTO> ListTodoItems()
        {
            return _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToList();
        }

        public TodoItemDTO? FindTodoItem(long id)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null)
            {
                return null;
            }

            return ItemToDTO(todoItem);
        }

        public void UpdateTodoItem(long id, TodoItemDTO todoDTO)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem == null)
            {
                throw new Exception("Not found");
            }

            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                throw;
            }
        }

        public TodoItemDTO CreateTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            //await _context.SaveChangesAsync();
            _context.SaveChanges();

            return ItemToDTO(todoItem);
        }

        public bool? DeleteTodoItem(long id)
        {
            var todoItem =  _context.TodoItems.Find(id);
            if (todoItem == null)
            {
                return null;
            }

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();

            return true;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };
    }
}
