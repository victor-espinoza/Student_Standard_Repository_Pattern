///////////////////////////////////////////////////////////////////////////////////////////////////////////
//Team Members: Victor Espinoza, Jeff Yoshida
//CECS 475 - Application Programming using .NET
//Assignment #5 - Repository Pattern
//Due: 3/17/16

using System;
using System.Collections.Generic;
using System.Linq;

namespace _475_Lab_4_Part_3 {
   class Program {
      static void Main(string[] args) {

         //Create User Input Variable
         int userInput = -1; //used to keep track of user input

         //Create a new business layer
         IBusinessLayer businessLayerInst = new BusinessLayer();
         int desiredStandardID = -1;
         string desiredStandardName, desiredStandardDescription, updateStandardName,
          updateStandardDescription;
         Standard newStandard, standardInst, updateStandard, deleteStandard;

         int desiredStudentID = -1, revisedStandardID;
         Student newStudent, updateStudent, deleteStudent;
         string updateStudentName, updateStandardID, desiredStudentName;

         while (userInput != 14) {
            try {
               //void RemoveStudent(Student removeStudentItem);
               //display commands and prompt user to enter a valid command
               Console.WriteLine("\n\nUser Menu: \n 1.  Display all Standards\n 2.  Get Standard by "
                 + "Standard ID\n 3.  Get Standard by Standard Name\n 4.  Add new Standard\n 5.  "
                 + "Udate Standard\n 6.  Remove Standard\n 7.  Display all Students\n 8.  Get "
                 + "Student By Student ID\n 9.  Get Student by Student Name\n 10. Get all Students "
                 + "that have the same Standard ID\n 11. Add new Student\n 12. Update Student\n "
                 + "13. Remove Student\n 14. Exit Program");
               Console.WriteLine("\nPlease enter the number of the command you wish to "
                + "execute:\n(1 <= command number =< 14): ");
               //attempt to convert the user input into an integer
               userInput = Convert.ToInt32(Console.ReadLine());
               //if the conversion was correct but the number is not within the valid
               //range of 1 <= input =< 14, then re-prompt the user to enter a valid value
               if (userInput > 14 || userInput < 1)
                  Console.WriteLine("The number provided was not within the appropriate"
                    + " range of permissible \nvalues. Please enter an integer value "
                    + "between 1 and 14...");
               else {
                  switch (userInput) {
                     case 1:
                        //Display all of the standard data 
                        Console.WriteLine("Retrieve all of the Standards using the "
                         + "getAllStandards() method:");
                        DisplayStandards(businessLayerInst.getAllStandards());
                        break;

                     case 2:
                        //Display standard data using getID method
                        Console.WriteLine("\n\nGet a Standard using the GetStandardByID(int id) "
                         + "method:");
                        //get valid standard id
                        desiredStandardID = ValidateStandardID(businessLayerInst);
                        ViewStandard(businessLayerInst.GetStandardByID(desiredStandardID));
                        break;

                     case 3:
                        //Display standard data using GetByStandardName method
                        Console.WriteLine("\n\nGet a Standard using the GetByStandardName(string "
                         + "standardName) method:");
                        //get valid standard id
                        desiredStandardName = ValidateStandardName(businessLayerInst);
                        ViewStandard(businessLayerInst.GetStandardByName(desiredStandardName));
                        break;

                     case 4:
                        //Add a new standard to the table
                        Console.WriteLine("\n\nAdd Standard using the addStandard(Standard "
                         + "addStandardItem) method. ");
                        //inform user of auto-increment standard id table column definition
                        Console.WriteLine("The Standard ID will automatically be created for "
                         + "you via the sql table definition.");
                        //get standard name from user
                        Console.WriteLine("Please Enter the Desired Standard Name:");
                        desiredStandardName = Console.ReadLine();
                        //get standard description from user
                        Console.WriteLine("Please Enter the Desired Standard Description:");
                        desiredStandardDescription = Console.ReadLine();

                        //create new standard using user information
                        newStandard = new Standard();
                        newStandard.StandardName = desiredStandardName;
                        newStandard.Description = desiredStandardDescription;
                        businessLayerInst.addStandard(newStandard);

                        //Display the newly created standard
                        Console.WriteLine("\nNew Standard created successfully. Here is the "
                         + "newly created standard:");
                        ViewStandard(newStandard);
                        break;

                     case 5:
                        //update Standard
                        Console.WriteLine("Udate Standard using the updateStandard(Standard "
                         + "updateStandardItem) method:");
                        //get valid standard id
                        desiredStandardID = ValidateStandardID(businessLayerInst);
                        //get standard to update
                        updateStandard = businessLayerInst.GetStandardByID(desiredStandardID);
                        Console.WriteLine("\n\nStandard Data Retrieved.");

                        //check to see if user wants to update standard name
                        updateStandardName = UpdateDataValidation("Standard Name");
                        //update Standard Name
                        if (!String.IsNullOrEmpty(updateStandardName))
                           updateStandard.StandardName = updateStandardName;

                        //check to see if user wants to update standard description
                        updateStandardDescription = UpdateDataValidation("Standard Description");
                        //update Standard Description
                        if (!String.IsNullOrEmpty(updateStandardDescription))
                           updateStandard.Description = updateStandardDescription;

                        //update standard
                        businessLayerInst.updateStandard(updateStandard);
                        //show user updated data
                        if (String.IsNullOrEmpty(updateStandardDescription) &&
                         String.IsNullOrEmpty(updateStandardName))
                           Console.WriteLine("Standard will remain unchanged.");
                        else
                           Console.WriteLine("Standard Updated Successfully.");
                        //show updated standard data
                        Console.WriteLine("\nHere is the Updated Standard data:");
                        ViewStandard(businessLayerInst.GetStandardByID(desiredStandardID));
                        break;

                     case 6:
                        //remove standard data
                        Console.WriteLine("Remove Standard using the removeStandard(Standard "
                         + "removeStandardItem) method.");
                        desiredStandardID = ValidateStandardID(businessLayerInst);
                        deleteStandard = businessLayerInst.GetStandardByID(desiredStandardID);

                        //Delete reference to all students who have the StandardID that is 
                        //going to be deleted.

                        //get student to update
                        Console.WriteLine("Deleting reference to all Students who are connected "
                         + "to the Standard that is going to be deleted....");
                        standardInst = businessLayerInst.GetStudentsByStandardID(
                         desiredStandardID);
                        foreach (Student studentItem in standardInst.Students) {
                           //get student to update
                           updateStudent = businessLayerInst.GetStudentByID(
                            studentItem.StudentID);
                           updateStudent.StandardId = null;
                           businessLayerInst.UpdateStudent(updateStudent);
                        }//close foreach                              

                        //Delete Standard
                        businessLayerInst.removeStandard(deleteStandard);
                        //Display remaining standard data 
                        Console.WriteLine("Standard successfully removed! Here is all of the "
                         + "remaining Standard data:");
                        DisplayStandards(businessLayerInst.getAllStandards());
                        break;

                     case 7:
                        //Display all of the student data 
                        Console.WriteLine("Retrieve all of the Students using the "
                         + "getAllStudents() method:");
                        DisplayStudents(businessLayerInst.getAllStudents());
                        break;

                     case 8:
                        //Display student data using getID method
                        Console.WriteLine("\n\nGet a Student using the GetStudentID(int id) "
                         + "method:");
                        //get valid standard id
                        desiredStudentID = ValidateStudentID(businessLayerInst);
                        ViewStudent(businessLayerInst.GetStudentByID(desiredStudentID));
                        break;

                     case 9:
                        //Display Student using GetStudentByName(string studentName) method
                        Console.WriteLine("\n\nGet a Student using the GetStudentByName(string "
                         + "studentName) method:");
                        //get valid standard id
                        desiredStudentName = ValidateStudentName(businessLayerInst);
                        ViewStudent(businessLayerInst.GetStudentByName(desiredStudentName));
                        break;

                     case 10:
                        //Get all students that have the same Standard ID

                        Console.WriteLine("\n\nGet all Students that have the same Standard ID:");
                        //get valid standard id
                        desiredStandardID = ValidateStandardID(businessLayerInst);
                        standardInst = businessLayerInst.GetStudentsByStandardID(
                         desiredStandardID);
                        if (!standardInst.Students.Any())
                           Console.WriteLine("No Students have the associated Standard ID!");
                        else
                           DisplayStudents(standardInst.Students.ToList<Student>());
                        break;

                     case 11:
                        //create new student
                        Console.WriteLine("\n\nAdd Student using addStudent(Student "
                         + "addStudentItem) method. ");
                        //inform user of auto-increment standard id table column definition
                        Console.WriteLine("The Student ID will automatically be created for you "
                         + "via the sql table definition.");
                        //get standard name from user
                        Console.WriteLine("Please Enter the Desired Student Name:");
                        desiredStudentName = Console.ReadLine();
                        //get standard description from user
                        Console.WriteLine("Please Enter the Desired Standard Associated with the "
                         + "student (StandardId must be a valid StandardId in the Standards "
                         + "table:");
                        //get valid standard id
                        desiredStandardID = ValidateStandardID(businessLayerInst);

                        //Add a new student to the table
                        newStudent = new Student();
                        newStudent.StudentName = desiredStudentName;
                        newStudent.StandardId = desiredStandardID;
                        businessLayerInst.addStudent(newStudent);

                        //Display the newly created student
                        Console.WriteLine("\nNew Student created successfully. Here is the newly "
                         + "created student:");
                        ViewStudent(newStudent);
                        break;

                     case 12:
                        //update student
                        Console.WriteLine("Udate Student using the UpdateStudent(Student "
                         + "updateStudentItem) method:");
                        //get valid student id
                        desiredStudentID = ValidateStudentID(businessLayerInst);
                        //get student to update
                        updateStudent = businessLayerInst.GetStudentByID(desiredStudentID);
                        Console.WriteLine("\n\nStudent Data Retrieved.");

                        //check to see if user wants to update student name
                        updateStudentName = UpdateDataValidation("Student Name");
                        //update student name 
                        if (!String.IsNullOrEmpty(updateStudentName))
                           updateStudent.StudentName = updateStudentName;

                        //check to see if user wants to update standard id
                        updateStandardID = "initialize";
                        while (!updateStandardID.ToUpper().Equals("Y") &&
                         !updateStandardID.ToUpper().Equals("N")) {
                           Console.WriteLine("Do you wish to update the Standard ID? y/n");
                           updateStandardID = Console.ReadLine();
                        }//end while

                        //prompt user to enter updated standard ID
                        if (updateStandardID.ToUpper().Equals("Y")) {
                           //validate standard ID
                           revisedStandardID = ValidateStandardID(businessLayerInst);
                           //update standard ID
                           updateStudent.StandardId = revisedStandardID;
                        }//end if

                        //update standard
                        businessLayerInst.UpdateStudent(updateStudent);
                        //show user updated data
                        if (String.IsNullOrEmpty(updateStudentName) &&
                         !updateStandardID.ToUpper().Equals("Y"))
                           Console.WriteLine("Student will remain unchanged.");
                        else
                           Console.WriteLine("Student Updated Successfully.");
                        //show updated standard data
                        Console.WriteLine("\nHere is the updated Student data:");
                        ViewStudent(businessLayerInst.GetStudentByID(desiredStudentID));
                        break;

                     case 13:
                        //remove student data
                        Console.WriteLine("Remove Student using the RemoveStudent(Student "
                         + "removeStudentItem) method.");
                        desiredStudentID = ValidateStudentID(businessLayerInst);
                        deleteStudent = businessLayerInst.GetStudentByID(desiredStudentID);
                        businessLayerInst.RemoveStudent(deleteStudent);

                        //Display remaining standard data 
                        Console.WriteLine("Student Successfully Removed. Here is all of the "
                         + "remaining Student data:");
                        DisplayStudents(businessLayerInst.getAllStudents());
                        break;

                     case 14:
                        Console.WriteLine("You chose command #14:\nYou will now exit the "
                         + "program...");
                        break;
                     default:
                        Console.WriteLine("You have reached the default case...");
                        break;
                  } //end case
               } //end else
            } //end try
            catch (FormatException) {
               //inform the user that they did not enter an integer value and re-prompt
               //the command value input.
               Console.WriteLine("Invalid user input. Please enter an INTEGER value "
                + "between 1 and 14...");
            } //end catch
         } //end while loop

         Console.WriteLine("Your session has been terminated. Thank you for using this "
          + "program.\nClick any key to close this window..");
         Console.ReadKey();
      } //close Main(...)


      static string ValidateStandardName(IBusinessLayer businessLayerInst) {
         //get valid standard name
         string desiredStandardName = "initialize";
         Standard standardName = null;
         IList<Standard> allStandardNames;
         while (standardName == null) {
            //Display all of the standard data 
            Console.WriteLine("Available Standard Names to choose from:");
            //Display all of the standards
            allStandardNames = businessLayerInst.getAllStandards();
            Console.WriteLine("{0, -20}", "Standard Name:");
            foreach (var listItem in allStandardNames)
               Console.WriteLine("{0, -20}", listItem.StandardName);
            Console.Write("\nPlease Enter the Desired StandardName of the desired Standard:");
            //attempt to convert the user input into an integer
            desiredStandardName = Console.ReadLine();

            //attempt to retrieve the standard by the standardname input from user.
            standardName = businessLayerInst.GetStandardByName(desiredStandardName);
         } //end while
         return desiredStandardName;
      } //close method ValidateStandardName(...)


      static int ValidateStandardID(IBusinessLayer businessLayerInst) {
         IList<Standard> allStandardIDs;
         //get valid standard id
         int desiredStandardID = -1;
         Standard updateStandard = null;
         while (updateStandard == null) {
            try {
               //Display all of the standard data 
               Console.WriteLine("Available Standards to choose from:");
               //Display all of the standards
               allStandardIDs = businessLayerInst.getAllStandards();
               Console.WriteLine("{0, -20}", "Standard ID:");
               foreach (var listItem in allStandardIDs)
                  Console.WriteLine("{0, -20}", listItem.StandardId);

               Console.Write("\nPlease Enter the Desired StandardID of the desired Standard:");
               //attempt to convert the user input into an integer
               desiredStandardID = Convert.ToInt32(Console.ReadLine());

               //attempt to retrieve the standard by the standardid input from user.
               updateStandard = businessLayerInst.GetStandardByID(desiredStandardID);
               //if the conversion was correct but the number is not within the valid
               //range of DesiredID, then re-prompt the user to enter a valid value
               if (updateStandard == null)
                  Console.WriteLine("The number provided was not within the appropriate"
                   + " range of permissible \nvalues. Please enter an integer value "
                   + "that corresponds to a valid StandardID...");
            } //end try
            catch (FormatException) {
               //inform the user that they did not enter an integer value and re-prompt
               //the command value input.
               Console.WriteLine("Invalid user input. Please enter an INTEGER value "
                + "that corresponds to a valid StandardID...");
            } //end catch
         } //end while
         return desiredStandardID;
      } //close ValidateStandardID(...) 


      static void DisplayStandards(IList<Standard> allStandards) {
         Console.WriteLine("{0, -20} {1,-20} {2,-30}", "Standard ID:", "Standard Name:",
          "Description:");
         foreach (var listItem in allStandards)
            Console.WriteLine("{0, -20} {1,-20} {2,-30}", listItem.StandardId, listItem.StandardName,
             listItem.Description);
      } //close DisplayStandards(...) 


      static void ViewStandard(Standard standardInst) {
         Console.WriteLine("{0, -20} {1,-20} {2,-30}", "Standard ID:", "Standard Name:",
          "Description:");
         Console.WriteLine("{0, -20} {1,-20} {2,-30}", standardInst.StandardId,
          standardInst.StandardName, standardInst.Description);
      } //close ViewStandard(...)


      static string UpdateDataValidation(string condition) {
         //check to see if user wants to update standard name
         string updateData = "initialize";
         while (!updateData.ToUpper().Equals("Y") && !updateData.ToUpper().Equals("N")) {
            Console.WriteLine("Do you wish to update the " + condition + "? y/n");
            updateData = Console.ReadLine();
         } //end while

         //prompt user to enter updated name if desired
         if (updateData.ToUpper().Equals("Y")) {
            Console.WriteLine("Please Enter the Desired " + condition + ":");
            //update standard name
            return Console.ReadLine();
         } //end if
         return null;
      } //close UpdateDataValidation(...)


      static int ValidateStudentID(IBusinessLayer businessLayerInst) {
         IList<Student> allStudentIDs;
         //get valid student id
         int desiredStudentID = -1;
         Student updateStudent = null;
         while (updateStudent == null) {
            try {
               //Display all of the student data 
               Console.WriteLine("Available Students to choose from:");
               //Display all of the student ID's
               allStudentIDs = businessLayerInst.getAllStudents();
               Console.WriteLine("{0, -20}", "Student ID:");
               foreach (var listItem in allStudentIDs)
                  Console.WriteLine("{0, -20}", listItem.StudentID);

               Console.Write("\nPlease Enter the Desired StudentID of the desired Student:");
               //attempt to convert the user input into an integer
               desiredStudentID = Convert.ToInt32(Console.ReadLine());

               //attempt to retrieve the standard by the standardid input from user.
               updateStudent = businessLayerInst.GetStudentByID(desiredStudentID);
               //if the conversion was correct but the number is not within the valid
               //range of DesiredID, then re-prompt the user to enter a valid value
               if (updateStudent == null)
                  Console.WriteLine("The number provided was not within the appropriate"
                   + " range of permissible \nvalues. Please enter an integer value "
                   + "that corresponds to a valid StudentID...");
            } //end try
            catch (FormatException) {
               //inform the user that they did not enter an integer value and re-prompt
               //the command value input.
               Console.WriteLine("Invalid user input. Please enter an INTEGER value "
                + "that corresponds to a valid StudentID...");
            } //end catch
         } //end while
         return desiredStudentID;
      } //close ValidateStudentID(...) 


      static void DisplayStudents(IList<Student> allStudents) {
         Console.WriteLine("{0, -20} {1,-15} {2,-15} {3, -20}", "Student ID:", "Student Name:",
          "Standard ID:", "Row Version:");
         foreach (var listItem in allStudents)
            Console.WriteLine("{0, -20} {1,-15} {2,-15} {3, -20}", listItem.StudentID,
             listItem.StudentName, listItem.StandardId, BitConverter.ToString(listItem.RowVersion));
      } //close DisplayStudents(...) 


      static void ViewStudent(Student studentInst) {
         Console.WriteLine("{0, -20} {1,-15} {2,-15} {3, -20}", "Student ID:", "Student Name:",
          "Standard ID:", "Row Version:");
         Console.WriteLine("{0, -20} {1,-15} {2,-15} {3, -20}", studentInst.StudentID,
          studentInst.StudentName, studentInst.StandardId,
          BitConverter.ToString(studentInst.RowVersion));
      } //close ViewStudent(...)


      static string ValidateStudentName(IBusinessLayer businessLayerInst) {
         Student studentName = null;
         //get valid standard name
         string desiredStudentName = "initialize";
         IList<Student> studentsByName = new List<Student>();
         while (studentName == null) {
            //Display all of the standard data 
            Console.WriteLine("Available Standard Names to choose from:");
            //Display all of the student names
            studentsByName = businessLayerInst.getAllStudents();
            Console.WriteLine("{0, -20}", "Student Name:");
            foreach (var listItem in studentsByName)
               Console.WriteLine("{0, -20}", listItem.StudentName);
            Console.Write("\nPlease Enter the Desired StandardName of the desired Standard:");
            //attempt to convert the user input into an integer
            desiredStudentName = Console.ReadLine();

            //attempt to retrieve the standard by the standardname input from user.
            studentName = businessLayerInst.GetStudentByName(desiredStudentName);
         } //end while
         return desiredStudentName;
      } //close ValidateStudentName(...) 

   } //close class Program

} //close namespace _475_Lab_4_Part_3
