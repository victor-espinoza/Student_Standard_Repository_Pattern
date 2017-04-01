//Team Members: Victor Espinoza, Jeff Yoshida
//CECS 475 - Application Programming using .NET
//Assignment #5 - Repository Pattern
//Due: 3/17/16

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace _475_Lab_4_Part_3 {
   public interface IStandardRepository : IRepository<Standard> {


   } //close IStandardRepository interface


   public interface IStudentRepository : IRepository<Student> {

   } //close IStudentRepository interface


   public class StandardRepository : Repository<Standard>, IStandardRepository {
      public StandardRepository()
         : base(new SchoolDBEntities()) {

      } //close StandardRepository() constructor

   } //close StandardRepository class


   public class StudentRepository : Repository<Student>, IStudentRepository {
      public StudentRepository()
         : base(new SchoolDBEntities()) {

      } //close StudentRepository() constructor

   } //close StudentRepository class


} //close namespace _475_Lab_4_Part_3
