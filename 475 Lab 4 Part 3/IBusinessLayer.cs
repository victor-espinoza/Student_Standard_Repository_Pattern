//Team Members: Victor Espinoza, Jeff Yoshida
//CECS 475 - Application Programming using .NET
//Assignment #5 - Repository Pattern
//Due: 3/17/16

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace _475_Lab_4_Part_3 {

   public interface IBusinessLayer {
      IList<Standard> getAllStandards();
      Standard GetStandardByID(int id);
      Standard GetStandardByName(string standardName);
      void addStandard(Standard addStandardItem);
      void updateStandard(Standard updateStandardItem);
      void removeStandard(Standard removeStandardItem);
      IList<Student> getAllStudents();
      Student GetStudentByID(int id);
      Student GetStudentByName(string studentName);
      void addStudent(Student addStudentItem);
      void UpdateStudent(Student updateStudentItem);
      void RemoveStudent(Student removeStudentItem);
      Standard GetStudentsByStandardID(int standardID);
   } //close interface IBusinessLayer

   public class BusinessLayer : IBusinessLayer {
      private readonly IStandardRepository _standardRepository;
      private readonly IStudentRepository _studentRepository;

      public BusinessLayer() {
         _standardRepository = new StandardRepository();
         _studentRepository = new StudentRepository();
      } //close BusinessLayer() constructor


      public IList<Standard> getAllStandards() {
         return _standardRepository.GetAll().ToList<Standard>();
      } //close getAllStandards() 


      public Standard GetStandardByID(int id) {
         return _standardRepository.GetById(id);
      } //close GetStandardByID(...) 


      public Standard GetStandardByName(string standardName) {
         return _standardRepository.GetSingle(
             d => d.StandardName.Equals(standardName),
             d => d.Students); //include related students
      } //close GetStandardByName(...)


      public void addStandard(Standard addStandardItem) {
         _standardRepository.Insert(addStandardItem);
      } //close addStandard(...) 


      public void updateStandard(Standard updateStandardItem) {
         _standardRepository.Update(updateStandardItem);
      } //close updateStandard(...) 


      public void removeStandard(Standard removeStandardItem) {
         _standardRepository.Delete(removeStandardItem);
      } //close removeStandard(...) 


      public Standard GetStudentsByStandardID(int standardID) {
         return _standardRepository.GetSingle(
             d => d.StandardId == standardID,
             d => d.Students); //include related Students
      } //close GetStudentsByStandardID(...)


      public IList<Student> getAllStudents() {
         return _studentRepository.GetAll().ToList<Student>();
      } //close getAllStudents() 


      public Student GetStudentByID(int id) {
         return _studentRepository.GetById(id);
      } //close GetStudentByID(...) 


      public Student GetStudentByName(string studentName) {
         return _studentRepository.GetSingle(
          d => d.StudentName.Equals(studentName),
          d => d.Standard); //include related standards
      } //close GetStudentByName(...) 


      public void addStudent(Student addStudentItem) {
         _studentRepository.Insert(addStudentItem);
      } //close addStudent(...)


      public void UpdateStudent(Student updateStudentItem) {
         _studentRepository.Update(updateStudentItem);
      } //close UpdateStudent(...)


      public void RemoveStudent(Student removeStudentItem) {
         _studentRepository.Delete(removeStudentItem);
      } //close RemoveStudent(...) 

   } //close class BusinessLayer

} //close namespace _475_Lab_4_Part_3
