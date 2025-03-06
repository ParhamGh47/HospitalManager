This is a Hospital or Clinic Manager with C# Windows Form and SQL database.

The tables inside the Hospital database are:
Doctor : ID(PK), First_Name, Last_Name, Phone, Department_ID(FK), Is_Deleted
Department : ID(PK), Name
Specialization : ID(PK), Name, Doctor_ID(FK)
Patient : ID(PK), First_Name, Last_Name, Gender, Birthday, Phone, Contact, BloodType, Is_Deleted
Payment : ID(PK), Patient_ID(FK), Amount, Date_Issued, Status, Description
Appointment : ID(PK), Patient_ID(FK), Doctor_ID(FK), Date, Status, Description
Medicine : ID(PK), Name, Appointment_ID(FK), Dosage, Start_Date, End_Date, Description

- DataTypes: All the string datas are nvarchar and all the numbers are bigint among with some datetime values. Description value is optional in all tables.

IMPORTANT: For the application to work, you need to edit the connectionString located in Connection.cs.
