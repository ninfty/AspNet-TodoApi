using AutoMapper;
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
        private IMapper _mapper;

        public TodoItemsService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public List<TodoDTO> ListTodoItems()
        {
            return _todoRepository.GetAll()
                .Select(x => _mapper.Map<TodoDTO>(x))
                .ToList();
        }

        public TodoDTO? FindTodoItem(long id)
        {
            var todoItem = _todoRepository.GetById(id);

            if (todoItem == null) {
                throw new Exception("Not found");
            }

            return _mapper.Map<TodoDTO>(todoItem);
        }

        public void UpdateTodoItem(long id, TodoDTO todoDTO)
        {
            try
            {
                _todoRepository.Update(id, _mapper.Map<TodoItem>(todoDTO));
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

            return _mapper.Map<TodoDTO>(todoItem);
        }

        public bool? DeleteTodoItem(long id)
        {
            var deleted =  _todoRepository.Delete(id);

            if (!deleted) {
                throw new Exception("Not found");
            }

            return true;
        }

    }
}
