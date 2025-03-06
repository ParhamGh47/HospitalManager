using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Transactions;


namespace Hospital
{
    internal class Connection
    {
        private readonly string connectionString = @"";


        #region Search

        public DataTable SearchPatient(string firstName, string lastName, bool IncludeDeleted = true)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query;

                if (IncludeDeleted)
                {
                    query = @"SELECT ID, First_Name, Last_Name, Phone, Gender, Birthday, Blood_Type, Contact, Is_Deleted 
                      FROM Patient 
                      WHERE (@FirstName IS NULL OR First_Name LIKE '%' + @FirstName + '%')
                        AND (@LastName IS NULL OR Last_Name LIKE '%' + @LastName + '%')";
                }
                else
                {
                    query = @"SELECT ID, First_Name, Last_Name, Phone, Gender, Birthday, Blood_Type, Contact
                      FROM Patient
                      WHERE (@FirstName IS NULL OR First_Name LIKE '%' + @FirstName + '%')
                        AND (@LastName IS NULL OR Last_Name LIKE '%' + @LastName + '%')
                        AND Is_Deleted = 0";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? (object)DBNull.Value : firstName);
                    command.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(lastName) ? (object)DBNull.Value : lastName);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
            }
        }



        public DataTable SearchDoctor(string firstName, string lastName, bool IncludeDeleted)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query;

                if (IncludeDeleted)
                {
                    query = @"SELECT ID, First_Name, Last_Name, Phone, Department_ID, Is_Deleted 
                      FROM Doctor 
                      WHERE (@FirstName IS NULL OR First_Name LIKE '%' + @FirstName + '%')
                        AND (@LastName IS NULL OR Last_Name LIKE '%' + @LastName + '%')";
                }
                else
                {
                    query = @"SELECT ID, First_Name, Last_Name, Phone, Department_ID, Is_Deleted 
                      FROM Doctor 
                      WHERE (@FirstName IS NULL OR First_Name LIKE '%' + @FirstName + '%')
                        AND (@LastName IS NULL OR Last_Name LIKE '%' + @LastName + '%')
                        AND Is_Deleted = 0";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? (object)DBNull.Value : firstName);
                    command.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(lastName) ? (object)DBNull.Value : lastName);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
            }
        }


        public DataTable SearchDepartmentRemove(string Name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT Name, ID FROM Department 
                         WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(Name) ? (object)DBNull.Value : Name);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
            }
        }



        public List<string> GetDepartmentNames(Int64 id = 0)
        {
            List<string> departments = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Department";

                if (id != 0)
                {
                    query += " WHERE ID = @ID";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (id != 0)
                    {
                        command.Parameters.AddWithValue("@ID", id);
                    }

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(reader["Name"].ToString());
                        }
                    }
                }
            }

            return departments;
        }






        public DataTable SearchAppointments(string Status, string doctorName = "", string doctorLast = "", string patientName = "", string patientLast = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT A.ID, 
                                A.Date, 
                                A.Status, 
                                CONCAT(D.First_Name, ' ', D.Last_Name) AS Doctor_Name, 
                                CONCAT(P.First_Name, ' ', P.Last_Name) AS Patient_Name, 
                                A.Description
                         FROM Appointment A
                         INNER JOIN Doctor D ON A.Doctor_ID = D.ID
                         INNER JOIN Patient P ON A.Patient_ID = P.ID
                         WHERE (@DoctorName IS NULL OR D.First_Name LIKE '%' + @DoctorName + '%')
                           AND (@DoctorLast IS NULL OR D.Last_Name LIKE '%' + @DoctorLast + '%')
                           AND (@PatientName IS NULL OR P.First_Name LIKE '%' + @PatientName + '%')
                           AND (@PatientLast IS NULL OR P.Last_Name LIKE '%' + @PatientLast + '%')";

                if (Status != "All")
                {
                    query += " AND A.Status = @Status";
                }

                query += " ORDER BY A.Date DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorName", string.IsNullOrEmpty(doctorName) ? (object)DBNull.Value : doctorName);
                    command.Parameters.AddWithValue("@DoctorLast", string.IsNullOrEmpty(doctorLast) ? (object)DBNull.Value : doctorLast);
                    command.Parameters.AddWithValue("@PatientName", string.IsNullOrEmpty(patientName) ? (object)DBNull.Value : patientName);
                    command.Parameters.AddWithValue("@PatientLast", string.IsNullOrEmpty(patientLast) ? (object)DBNull.Value : patientLast);

                    if (Status != "All")
                    {
                        command.Parameters.AddWithValue("@Status", Status);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
            }
        }


        public DataTable SearchSpecialization(Int64 doctorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT Name, ID
                         FROM Specialization
                         WHERE Doctor_ID = @DoctorID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);

                    return resultTable;
                }
            }
        }


        public DataTable SearchMedicine(Int64 appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ID, Name, Start_Date, End_Date, Dosage, Description 
                         FROM Medicine 
                         WHERE Appointment_ID = @AppointmentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    return resultTable;
                }
            }
        }



        public void SearchPayment(Int64 patientId, out string status, out string description, out long amount, out DateTime dateIssued)
        {
            status = string.Empty;
            description = string.Empty;
            amount = 0;
            dateIssued = new DateTime();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT Status, Description, Amount, Date_Issued 
                     FROM Payment 
                     WHERE Patient_ID = @PatientID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                status = reader.GetString(reader.GetOrdinal("Status"));
                                description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description"));
                                amount = reader.GetInt64(reader.GetOrdinal("Amount"));
                                dateIssued = reader.GetDateTime(reader.GetOrdinal("Date_Issued"));
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        
                        Console.WriteLine($"Database error: {ex.Message}"); 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }
                }
            }
        }



        #endregion


        #region Insert
        public static Int64 LastInsertedDoctorId { get; private set; }

        public void InsertPatient(string firstName, string lastName, string gender, DateTime birthday, string phone, string contact, string bloodType, DateTime paymentDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string patientQuery = @"
            INSERT INTO Patient (First_Name, Last_Name, Gender, Birthday, Phone, Contact, Blood_Type) 
            VALUES (@FirstName, @LastName, @Gender, @Birthday, @Phone, @Contact, @BloodType);
            SELECT SCOPE_IDENTITY();";

                string paymentQuery = @"
            INSERT INTO Payment (Patient_ID, Amount, Status, Date_Issued) 
            VALUES (@PatientID, @Amount, @Status, @DateIssued);";

                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                        using (SqlCommand patientCommand = new SqlCommand(patientQuery, connection, transaction))
                        {
                            patientCommand.Parameters.AddWithValue("@FirstName", firstName);
                            patientCommand.Parameters.AddWithValue("@LastName", lastName);
                            patientCommand.Parameters.AddWithValue("@Gender", gender);
                            patientCommand.Parameters.AddWithValue("@Birthday", birthday);
                            patientCommand.Parameters.AddWithValue("@Phone", phone);
                            patientCommand.Parameters.AddWithValue("@Contact", contact);
                            patientCommand.Parameters.AddWithValue("@BloodType", bloodType);

                            Int64 patientId = Convert.ToInt64(patientCommand.ExecuteScalar());

                            using (SqlCommand paymentCommand = new SqlCommand(paymentQuery, connection, transaction))
                            {
                                paymentCommand.Parameters.AddWithValue("@PatientID", patientId);
                                paymentCommand.Parameters.AddWithValue("@DateIssued", paymentDate);
                                paymentCommand.Parameters.AddWithValue("@Amount", 0);
                                paymentCommand.Parameters.AddWithValue("@Status", "Pending");
                                paymentCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                }
            }
        }



        public Int64 InsertDoctor(string firstName, string lastName, string phone, string department)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                INSERT INTO Doctor (First_Name, Last_Name, Phone, Department_ID)
                VALUES (@FirstName, @LastName, @Phone, 
                    (SELECT ID FROM Department WHERE Name = @Department));
                SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Department", department);

                    connection.Open();
                    LastInsertedDoctorId = Convert.ToInt64(command.ExecuteScalar());
                    return LastInsertedDoctorId;
                }
            }
        }



        public void InsertDepartment(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Department (Name) VALUES (@Name)";



                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        public void InsertSpecialization(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Specialization (Name, Doctor_ID) VALUES (@Name, @DoctorID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@DoctorID", LastInsertedDoctorId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertSpecializationCustom(string name, Int64 doctorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Specialization (Name, Doctor_ID) VALUES (@Name, @DoctorID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@DoctorID", doctorId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void InsertAppointment(Int64 patient, Int64 doctor, DateTime dateTime, string description = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Appointment (Patient_ID, Doctor_ID, Date, Status, Description) 
                         VALUES (@Patient_ID, @Doctor_ID, @Date, @Status, @Description)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Patient_ID", patient);
                    command.Parameters.AddWithValue("@Doctor_ID", doctor);
                    command.Parameters.AddWithValue("@Date", dateTime);
                    command.Parameters.AddWithValue("@Status", "Upcoming");
                    command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(description) ? DBNull.Value : description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void InsertMedicine(string name, Int64 appointmentId, DateTime startDate, DateTime endDate, string dosage, string description = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Medicine (Name, Appointment_ID, Start_Date, End_Date, Dosage, Description) 
                         VALUES (@Name, @AppointmentId, @StartDate, @EndDate, @Dosage, @Description)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@Dosage", dosage);
                    command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion


        #region Update

        public void UpdateAppointment(Int64 appointmentId, string status, string description, DateTime? newDate = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query;

                if (status == "Upcoming" && newDate.HasValue)
                {
                    query = @"UPDATE Appointment
                      SET Status = @Status,
                          Date = @Date,
                          Description = @Description
                      WHERE ID = @AppointmentId";
                }
                else if (status == "Canceled" || status == "Completed")
                {
                    query = @"UPDATE Appointment
                      SET Status = @Status,
                          Description = @Description
                      WHERE ID = @AppointmentId";
                }
                else
                {
                    throw new ArgumentException("Invalid status or missing date for 'Upcoming' status.");
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    if (status == "Upcoming" && newDate.HasValue)
                    {
                        command.Parameters.AddWithValue("@Date", newDate.Value);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMedicine(Int64 medicineId, string name, DateTime startDate, DateTime endDate, string dosage, string description = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Medicine 
                         SET Name = @Name, 
                             Start_Date = @StartDate, 
                             End_Date = @EndDate, 
                             Dosage = @Dosage, 
                             Description = @Description 
                         WHERE ID = @MedicineId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicineId", medicineId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@Dosage", dosage);
                    command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }



        public void UpdatePatient(Int64 id, string firstName, string lastName, string gender, string bloodType, string phone, string contact, DateTime birthday)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Patient
                         SET First_Name = @FirstName, 
                             Last_Name = @LastName, 
                             Gender = @Gender, 
                             Blood_Type = @BloodType, 
                             Phone = @Phone, 
                             Contact = @Contact, 
                             Birthday = @Birthday
                         WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@BloodType", bloodType);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Contact", contact);
                    command.Parameters.AddWithValue("@Birthday", birthday);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }



        public void UpdateDoctor(Int64 id, string firstName, string lastName, string phone, string departmentName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string getDepartmentIdQuery = "SELECT ID FROM Department WHERE Name = @DepartmentName";
                Int64 departmentId;

                using (SqlCommand getDeptCommand = new SqlCommand(getDepartmentIdQuery, connection))
                {
                    getDeptCommand.Parameters.AddWithValue("@DepartmentName", departmentName);
                    connection.Open();
                    object result = getDeptCommand.ExecuteScalar();
                    connection.Close();

                    if (result == null)
                        throw new Exception("The specified department does not exist.");
                    departmentId = Convert.ToInt64(result);
                }

                string updateDoctorQuery = @"UPDATE Doctor
                                     SET First_Name = @FirstName, 
                                         Last_Name = @LastName, 
                                         Phone = @Phone, 
                                         Department_ID = @DepartmentID
                                     WHERE ID = @ID";

                using (SqlCommand updateDoctorCommand = new SqlCommand(updateDoctorQuery, connection))
                {
                    updateDoctorCommand.Parameters.AddWithValue("@ID", id);
                    updateDoctorCommand.Parameters.AddWithValue("@FirstName", firstName);
                    updateDoctorCommand.Parameters.AddWithValue("@LastName", lastName);
                    updateDoctorCommand.Parameters.AddWithValue("@Phone", phone);
                    updateDoctorCommand.Parameters.AddWithValue("@DepartmentID", departmentId);

                    connection.Open();
                    updateDoctorCommand.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSpecialization(Int64 id, string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Specialization
                         SET Name = @Name
                         WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateDepartment(Int64 id, string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Department
                         SET Name = @Name
                         WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdatePayment(Int64 patientId, string status, decimal amount, DateTime dateIssued, string description = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            UPDATE Payment
            SET Status = @Status, 
                Amount = @Amount, 
                Date_Issued = @DateIssued, 
                Description = @Description
            WHERE Patient_ID = @PatientId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@DateIssued", dateIssued);
                    command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                    command.Parameters.AddWithValue("@PatientId", patientId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }





        #endregion


        #region Remove

        public void RemovePatient(Int64 patientId, bool permanently)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (permanently)
                        {
                            string deletePaymentsQuery = "DELETE FROM Payment WHERE Patient_ID = @PatientID";
                            using (SqlCommand deletePaymentsCommand = new SqlCommand(deletePaymentsQuery, connection, transaction))
                            {
                                deletePaymentsCommand.Parameters.AddWithValue("@PatientID", patientId);
                                deletePaymentsCommand.ExecuteNonQuery();
                            }

                            string deletePatientQuery = "DELETE FROM Patient WHERE ID = @PatientID";
                            using (SqlCommand deletePatientCommand = new SqlCommand(deletePatientQuery, connection, transaction))
                            {
                                deletePatientCommand.Parameters.AddWithValue("@PatientID", patientId);
                                deletePatientCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string softDeleteQuery = "UPDATE Patient SET Is_Deleted = 1 WHERE ID = @PatientID";
                            using (SqlCommand softDeleteCommand = new SqlCommand(softDeleteQuery, connection, transaction))
                            {
                                softDeleteCommand.Parameters.AddWithValue("@PatientID", patientId);
                                softDeleteCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }



        public void RemoveDoctor(Int64 doctorId, bool permanently) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (permanently)
                        {
                            string deleteSpecializatons = "DELETE FROM Specialization WHERE Doctor_ID = @DoctorID";
                            using (SqlCommand deleteSpecialCommand = new SqlCommand(deleteSpecializatons, connection, transaction))
                            {
                                deleteSpecialCommand.Parameters.AddWithValue("@DoctorID", doctorId);
                                deleteSpecialCommand.ExecuteNonQuery();
                            }

                            string deleteDoctorquery = "DELETE FROM Doctor WHERE ID = @DoctorID";
                            using (SqlCommand deleteDoctorCommand = new SqlCommand(deleteDoctorquery, connection, transaction))
                            {
                                deleteDoctorCommand.Parameters.AddWithValue("@DoctorID", doctorId);
                                deleteDoctorCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string softDeleteQuery = "UPDATE Doctor SET Is_Deleted = 1 WHERE ID = @DoctorID";
                            using (SqlCommand softDeleteCommand = new SqlCommand(softDeleteQuery, connection, transaction))
                            {
                                softDeleteCommand.Parameters.AddWithValue("@DoctorID", doctorId);
                                softDeleteCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public void RemoveDepartment(Int64 id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Department WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void RemoveAppointment(Int64 id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Appointment WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveMedicine(Int64 medicineId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM Medicine WHERE ID = @MedicineId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicineId", medicineId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSpecialization(Int64 id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM Specialization WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}
