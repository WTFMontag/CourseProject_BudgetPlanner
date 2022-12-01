﻿using BudgetPlanner.Clases;
using BudgetPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Validation
{
    public abstract class Validation
    {
        private static decimal _result = 0;
        public abstract string? DataBaseName { get; set; }
        public abstract void Add();
        public abstract void Remove();
        public abstract void Alter();
        public abstract void Show();
        public static string CorrectString()
        {
            string? data = Console.ReadLine();
            bool IsValid = string.IsNullOrWhiteSpace(data);
            if (IsValid)
            {
                Console.WriteLine("Wrong input! Your input cannot be empty!");
                CorrectString();
            }
            return data!;
        }
        public static decimal CorrectNumber()
        {
            bool IsValid = decimal.TryParse(Console.ReadLine(), out _result);
            if (!IsValid)
            {
                Console.WriteLine("Wrong input! Try write number");
                CorrectNumber();
            }
            return _result;
        }
        public static bool IsCorrectUser(out int id)
        {
            bool isCorrectUser;
            IShowData iShow = new PersonValidation();
            iShow.Show();
            id = (int)CorrectNumber();
            using (MyBudgetPlannerContext db = new())
            {
                if (db.People.Find(id) != null)
                {
                    isCorrectUser = true;
                }
                else
                {
                    isCorrectUser = false;
                    Console.WriteLine($"Not found user with ID = {id}");
                }
            }
            return isCorrectUser;
        }
        public static bool IsAction()
        {
            bool isAction = false;
            Console.WriteLine("Do you want to continue?\n" +
                              "Write yes/no or y/n");
            string UserInput = CorrectString();
            switch (UserInput.ToLower())
            {
                case "yes":
                case "y":
                    isAction = true;
                    break;
                case "no":
                case "n":
                    isAction = false;
                    break;
            }
            return isAction;
        }
    }
}
