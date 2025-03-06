using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Hospital
{
    public partial class Dashboard : Form
    {
        private readonly Connection dbConnection;

        public Dashboard()
        {
            InitializeComponent();

            dbConnection = new Connection();

            DateCorrector();
            UiFix();
            ColorFix();

            LandingPanel.Show();
        }

        #region UI Related Updates

        private void UiFix()
        {
            paymentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            editPatientBlood.DropDownStyle = ComboBoxStyle.DropDownList;
            BloodAddPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            editDoctorDepartments.DropDownStyle = ComboBoxStyle.DropDownList;
            DepatmentAddDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            GenderAddPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            editPatientGender.DropDownStyle = ComboBoxStyle.DropDownList;
            StatusAppointmentM.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ColorFix()
        {
            GridPatientRemove.ForeColor = Color.Black;
            GridRemoveDoctor.ForeColor = Color.Black;
            GridRemoveDepartment.ForeColor = Color.Black;
            GridDoctorsAddApp.ForeColor = Color.Black;
            GridPatientsAddApp.ForeColor = Color.Black;
            GridRemoveAppointment.ForeColor = Color.Black;
            GridAppointments.ForeColor = Color.Black;
            GridAppointmentCompleted.ForeColor = Color.Black;
            GridMedicins.ForeColor = Color.Black;
            GridEditPatient.ForeColor = Color.Black;
            GridEditDoctors.ForeColor = Color.Black;
            GridSpeciaization.ForeColor = Color.Black;
            GridEditDepartment.ForeColor = Color.Black;
            GridPatientsPayment.ForeColor = Color.Black;
        }

        private void HideSections(object sender, EventArgs e)
        {
            SectionDivider.Hide();
            addPatient.Hide();
            addDoctor.Hide();
            addDepartment.Hide();
            RemovePatient.Hide();
            RemoveDoctor.Hide();
            DepartmentRemove.Hide();
            addAppoinment.Hide();
            AppointmentRemove.Hide();
            AppointmentManager.Hide();
            PrescriptionManager.Hide();
            EditPatient.Hide();
            EditDoctor.Hide();
            EditDepartment.Hide();
            PaymentManager.Hide();
            AboutPanel.Hide();
            LandingPanel.Hide();
        }


        private void HidePanels(object sender, EventArgs e)
        {
            panelEdit.Visible = false;
            panelNewRec.Visible = false;
            panelRemove.Visible = false;
        }

        private void HideMenuIcons(object sender, EventArgs e)
        {
            newRecIcon.Hide();
            editRecIcon.Hide();
            appoinmentsIcon.Hide();
            patientsProIcon.Hide();
            historyIcon.Hide();
            removeRecIcon.Hide();
            prescriptionIcon.Hide();
        }

        private void HideIconsAdd(object sender, EventArgs e)
        {
            iconAddPatient.Hide();
            iconAddDoctor.Hide();
            iconAddappoinment.Hide();
            iconAddDepartment.Hide();
        }

        private void HideIconsEdit(object sender, EventArgs e)
        {
            iconEditPatient.Hide();
            iconEditDoctor.Hide();
            iconEditDepartment.Hide();
        }


        private void HideIconRemove(object sender, EventArgs e)
        {
            iconRemPatient.Hide();
            iconRemDoctor.Hide();
            iconRemDepartment.Hide();
            iconRemAppointmnet.Hide();
        }


        private void editRecord_click(object sender, EventArgs e)
        {
            divider.Size = new System.Drawing.Size(2, 175);

            HideSections(sender, e);

            iconEditPatient.Hide();
            iconEditDoctor.Hide();
            iconEditDepartment.Hide();

            HideMenuIcons(sender, e);
            editRecIcon.Show();

            HidePanels(sender, e);
            panelEdit.BringToFront();
            panelEdit.Visible = true;

        }

        private void About_Click(object sender, EventArgs e)
        {
            divider.Size = new System.Drawing.Size(2, 450);

            HideMenuIcons(sender, e);
            historyIcon.Show();

            HidePanels(sender, e);
            HideSections(sender, e);
            AboutPanel.Show();
        }


        private void Appoinments_click(object sender, EventArgs e)
        {
            divider.Size = new System.Drawing.Size(2, 450);

            HideMenuIcons(sender, e);
            appoinmentsIcon.Show();

            HidePanels(sender, e);
            HideSections(sender, e);
            AppointmentManager.Show();
            GridAppointments.DataSource = null;
        }

        private void Prescription_Click(object sender, EventArgs e)
        {
            divider.Size = new System.Drawing.Size(2, 450);

            HideMenuIcons(sender, e);
            prescriptionIcon.Show();

            HidePanels(sender, e);
            HideSections(sender, e);
            PrescriptionManager.Show();

            GridMedicins.DataSource = null;
            GridAppointmentCompleted.DataSource = null;
        }


        private void newRecord_Click(object sender, EventArgs e)
        {
            divider.Size = new System.Drawing.Size(2, 215);

            HideSections(sender, e);

            iconAddPatient.Hide();
            iconAddDoctor.Hide();
            iconAddappoinment.Hide();
            iconAddDepartment.Hide();

            HideMenuIcons(sender, e);
            newRecIcon.Show();


            HidePanels(sender, e);
            panelNewRec.BringToFront();
            panelNewRec.Visible = true;

        }

        private void patientProfile_Click(object sender, EventArgs e)
        {
            divider.Size = new System.Drawing.Size(2, 450);

            HideMenuIcons(sender, e);
            patientsProIcon.Show();

            HidePanels(sender, e);
            HideSections(sender, e);
            PaymentManager.Show();
            GridPatientsPayment.DataSource = null;
        }

        private void removeRecord_Click(object sender, EventArgs e)
        {
            divider.Size = new System.Drawing.Size(2, 220);

            HideSections(sender, e);

            iconRemPatient.Hide();
            iconRemDoctor.Hide();
            iconRemDepartment.Hide();

            HideMenuIcons(sender, e);
            removeRecIcon.Show();

            HidePanels(sender, e);
            panelRemove.BringToFront();
            panelRemove.Visible = true;

        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            HideIconsAdd(sender, e);
            iconAddPatient.Show();

            HideSections(sender, e);
            addPatient.Show();
            SectionDivider.Show();
        }

        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            HideIconsAdd(sender, e);
            iconAddDoctor.Show();

            HideSections(sender, e);
            addDoctor.Show();
            SectionDivider.Show();

            DepatmentAddDoctor.Items.Clear();
            List<string> items = dbConnection.GetDepartmentNames();
            foreach (string item in items)
            {
                DepatmentAddDoctor.Items.Add(item);
            }

            SpecializationsLabelAddDoctor.Text = "Specializations: ";
            addSpecialAddDoctor.Enabled = false;
            SaveAddDoctor.Enabled = true;
        }

        private void btnAddAppoinment_Click(object sender, EventArgs e)
        {
            HideIconsAdd(sender, e);
            iconAddappoinment.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            addAppoinment.Show();

            GridDoctorsAddApp.DataSource = null;
            GridPatientsAddApp.DataSource = null;
        }


        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            HideIconsAdd(sender, e);
            iconAddDepartment.Show();

            HideSections(sender, e);
            addDepartment.Show();
            SectionDivider.Show();
        }


        private void btnEditPatient_Click(object sender, EventArgs e)
        {
            HideIconsEdit(sender, e);
            iconEditPatient.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            EditPatient.Show();

            GridEditPatient.DataSource = null;
        }

        private void btnEditDoctor_Click(object sender, EventArgs e)
        {
            HideIconsEdit(sender, e);
            iconEditDoctor.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            EditDoctor.Show();

            editDoctorDepartments.Items.Clear();
            List<string> items = dbConnection.GetDepartmentNames();
            foreach (string item in items)
            {
                editDoctorDepartments.Items.Add(item);
            }

            DoctorFirstAppM.Text = "";
            DoctorLastAppM.Text = "";
            PatientFirstAppM.Text = "";
            PatientLastAppM.Text = "";
            descrAppointmentM.Text = "";

            GridEditDoctors.DataSource = null;
            GridSpeciaization.DataSource = null;
        }


        private void btnEditDepatment_Click(object sender, EventArgs e)
        {
            HideIconsEdit(sender, e);
            iconEditDepartment.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            EditDepartment.Show();

            GridEditDepartment.DataSource = SearchDatabase("department");
            txtEditDepartment.Text = "";
            txtEditDepartment.Enabled = false;
        }


        private void btnRemPatient_Click(object sender, EventArgs e)
        {
            HideIconRemove(sender, e);
            iconRemPatient.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            RemovePatient.Show();

            GridPatientRemove.DataSource = null;
        }

        private void btnRemDoctor_Click(object sender, EventArgs e)
        {
            HideIconRemove(sender, e);
            iconRemDoctor.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            RemoveDoctor.Show();

            GridRemoveDoctor.DataSource = null;
        }

        private void btnRemDepartment_Click(object sender, EventArgs e)
        {
            HideIconRemove(sender, e);
            iconRemDepartment.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            DepartmentRemove.Show();

            GridRemoveDepartment.DataSource = null;
        }

        private void btnRemAppointment_Click(object sender, EventArgs e)
        {
            HideIconRemove(sender, e);
            iconRemAppointmnet.Show();

            HideSections(sender, e);
            SectionDivider.Show();
            AppointmentRemove.Show();

            GridRemoveAppointment.DataSource = dbConnection.SearchAppointments("Canceled");
        }


        #endregion


        #region Insert Record


        private void SaveAddPatient_Click(object sender, EventArgs e)
        {

            string firstName = FirstNAddPatient.Text;
            string lastName = LastNAddPatient.Text;
            string gender = GenderAddPatient.Text;
            DateTime birthday = BirthAddPatient.Value;
            string phone = PhoneAddPatient.Text;
            string contact = ContactAddPatient.Text;
            string bloodType = BloodAddPatient.Text;
            DateTime payment = DateTime.Now;


            if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(gender) ||
            string.IsNullOrWhiteSpace(phone) ||
            string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(bloodType))
            {
                MessageBox.Show("Please fill all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!StringValidation(firstName))
            {
                MessageBox.Show("First name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FirstNAddPatient.Text = "";
            }
            else if (!StringValidation(lastName))
            {
                MessageBox.Show("Last name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LastNAddPatient.Text = "";
            }
            else if (gender != "Male" && gender != "Female" && gender != "Other")
            {
                MessageBox.Show("Invalid gender!");
                GenderAddPatient.Text = "";
            }
            else if ((DateTime.Now.Year - birthday.Year) < 0 || (DateTime.Now.Year - birthday.Year) > 150)
            {
                MessageBox.Show("Please enter a valid birth date!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!PhoneValidation(phone))
            {
                MessageBox.Show("Phone number must be 11 digits containing only numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PhoneAddPatient.Text = "";

            }
            else if (!PhoneValidation(contact))
            {
                MessageBox.Show("Contact number must be 11 digits!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContactAddPatient.Text = "";
            }
            else if (!new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" }.Contains(bloodType))
            {
                MessageBox.Show("Invalid blood type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BloodAddPatient.Text = "";

            }
            else
            {
                try
                {
                    dbConnection.InsertPatient(firstName, lastName, gender, birthday, phone, contact, bloodType, payment);
                    MessageBox.Show("Patient and Payment records created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FirstNAddPatient.Clear();
                    LastNAddPatient.Clear();
                    GenderAddPatient.Text = "";
                    PhoneAddPatient.Clear();
                    ContactAddPatient.Clear();
                    BloodAddPatient.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



        }



        private void SaveAddDoctor_Click(object sender, EventArgs e)
        {
            string firstName = FirstNAddDoctor.Text;
            string lastName = LastNAddDoctor.Text;
            string phone = PhoneAddDoctor.Text;
            string department = DepatmentAddDoctor.Text;


            if (string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(department))
            {
                MessageBox.Show("Please fill all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!StringValidation(firstName))
            {
                MessageBox.Show("First name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FirstNAddDoctor.Text = "";
            }
            else if (!StringValidation(lastName))
            {
                MessageBox.Show("Last name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LastNAddDoctor.Text = "";
            }
            else if (!PhoneValidation(phone))
            {
                MessageBox.Show("Phone number must be 11 digits containing only numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PhoneAddDoctor.Text = "";
            }
            else if (!DepatmentAddDoctor.Items.Contains(department))
            {
                MessageBox.Show("Invalid Department!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DepatmentAddDoctor.Text = "";
            }
            else
            {
                try
                {
                    dbConnection.InsertDoctor(firstName, lastName, phone, department);
                    MessageBox.Show("Record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveAddDoctor.Enabled = false;
                    addSpecialAddDoctor.Enabled = true;
                    FirstNAddDoctor.Text = "";
                    LastNAddDoctor.Text = "";
                    DepatmentAddDoctor.Text = "";
                    PhoneAddDoctor.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }

        private void addSpecialAddDoctor_Click(object sender, EventArgs e)
        {
            string special = SpecializationAddDoctor.Text;

            if (!StringValidation(special))
            {
                MessageBox.Show("Invalid specialization!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SpecializationAddDoctor.Text = "";
            }
            else
            {
                try
                {
                    dbConnection.InsertSpecialization(special);
                    SpecializationAddDoctor.Text = "";
                    if (SpecializationsLabelAddDoctor.Text.EndsWith(": "))
                    {

                        SpecializationsLabelAddDoctor.Text = SpecializationsLabelAddDoctor.Text + special;

                    }
                    else
                    {
                        SpecializationsLabelAddDoctor.Text = SpecializationsLabelAddDoctor.Text + ", " + special;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }



        private void PatientSearchAddApp_Click(object sender, EventArgs e)
        {
            string first = txtFirstPatientAddApp.Text;
            string last = txtLastPatientAddApp.Text;


            DataTable patients = SearchDatabase("patients", first, last, false);

            if (patients.Rows.Count > 0)
            {
                GridPatientsAddApp.DataSource = patients;
            }
            else
            {
                MessageBox.Show("No matching patient found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridPatientsAddApp.DataSource = null;
            }

        }

        private void DoctorSearchAddApp_Click(object sender, EventArgs e)
        {
            string first = txtFirstDoctorAddApp.Text;
            string last = txtLastDoctorAddApp.Text;


            DataTable doctors = SearchDatabase("doctors", first, last, false);


            if (doctors.Rows.Count > 0)
            {
                GridDoctorsAddApp.DataSource = doctors;
            }
            else
            {
                MessageBox.Show("No matching doctor found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridDoctorsAddApp.DataSource = null;
            }
        }

        private void SaveAddDepartment_Click(object sender, EventArgs e)
        {
            string department = txtDepartmentAdd.Text;

            if (StringValidation(department))
            {
                try
                {
                    dbConnection.InsertDepartment(department);
                    MessageBox.Show("Record saved Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            txtDepartmentAdd.Text = "";
        }

        private void SaveAppoinmentBtn_Click(object sender, EventArgs e)
        {
            if (GridPatientsAddApp.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (GridDoctorsAddApp.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Int64 selectedPatientId = Convert.ToInt64(GridPatientsAddApp.SelectedRows[0].Cells["ID"].Value);
                Int64 selectedDoctorId = Convert.ToInt64(GridDoctorsAddApp.SelectedRows[0].Cells["ID"].Value);
                DateTime date = dateAddApp.Value;
                DateTime time = timeAddApp.Value;
                DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
                string description = DescriptionAddApp.Text;

                try
                {
                    dbConnection.InsertAppointment(selectedPatientId, selectedDoctorId, dateTime, description);
                    MessageBox.Show("Record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GridDoctorsAddApp.DataSource = null;
                    GridPatientsAddApp.DataSource = null;
                    txtFirstDoctorAddApp.Text = "";
                    txtFirstPatientAddApp.Text = "";
                    txtLastDoctorAddApp.Text = "";
                    txtLastPatientAddApp.Text = "";
                    DescriptionAddApp.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }



        private void AddMedicine_Click(object sender, EventArgs e)
        {
            if (GridAppointmentCompleted.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an appointment first!");
                return;

            }

            Int64 selectedAppId = Convert.ToInt64(GridAppointmentCompleted.SelectedRows[0].Cells["ID"].Value);
            string name = txtMedicineName.Text;
            string dosage = medicineDosage.Text;
            DateTime start = medicineStart.Value;
            DateTime end = medicineEnd.Value;
            string desc = medicineDescription.Text;


            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(dosage))
            {
                MessageBox.Show("Please fill all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    dbConnection.InsertMedicine(name, selectedAppId, start, end, dosage, desc);
                    MessageBox.Show("Medicine record created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMedicineName.Text = "";
                    medicineDosage.Text = "";
                    medicineDescription.Text = "";
                    GridMedicins.DataSource = dbConnection.SearchMedicine(selectedAppId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }



        private void AddSpecializationEditDoctorBtn_Click(object sender, EventArgs e)
        {
            Int64 doctorId = Convert.ToInt64(GridEditDoctors.SelectedRows[0].Cells["ID"].Value);
            string special = txtEditDoctorSpecialization.Text;

            if (special != "" && StringValidation(special))
            {
                try
                {
                    dbConnection.InsertSpecializationCustom(txtEditDoctorSpecialization.Text, doctorId);
                    MessageBox.Show("Specializations updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEditDoctorSpecialization.Text = "";
                    GridSpeciaization.DataSource = SearchDatabase("specialization", doctorId.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEditDoctorSpecialization.Text = "";
                }

            }
            else
            {
                MessageBox.Show("Invalid Specialization");
                txtEditDoctorSpecialization.Text = "";
            }
        }

        #endregion


        #region Remove Record

        private void RemovePatientSearch_Click(object sender, EventArgs e)
        {
            string firstName = FirstNRemovePatient.Text;
            string lastName = LastNRemovePatient.Text;

            DataTable patients = SearchDatabase("patients", firstName, lastName);

            if (patients.Rows.Count > 0)
            {
                GridPatientRemove.DataSource = patients;
            }
            else
            {
                MessageBox.Show("No matching patients found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridPatientRemove.DataSource = null;
            }
        }

        private void PatientRemove_Click(object sender, MouseEventArgs e)
        {
            if (GridPatientRemove.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedPatientId = Convert.ToInt64(GridPatientRemove.SelectedRows[0].Cells["ID"].Value);

            DialogResult dialogResult = MessageBox.Show(
                "The selected patient will be deleted.",
                "Confirmation",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
                DialogResult permanentDelete = MessageBox.Show(
                    "Do you want to permanently delete this record?",
                    "Permanently Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                bool isPermanent = permanentDelete == DialogResult.Yes;

                try
                {
                    dbConnection.RemovePatient(selectedPatientId, isPermanent);
                    MessageBox.Show("Patient removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FirstNRemovePatient.Text = "";
                    LastNRemovePatient.Text = "";
                    GridPatientRemove.DataSource = null;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("FK"))
                    {
                        MessageBox.Show($"Can't remove the data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }



        private void SearchRemoveDoctor_Click(object sender, EventArgs e)
        {
            string firstName = FirstNRemoveDoctor.Text;
            string lastName = LastNRemoveDoctor.Text;

            DataTable Doctors = SearchDatabase("doctors", firstName, lastName);

            if (Doctors.Rows.Count > 0)
            {
                GridRemoveDoctor.DataSource = Doctors;
            }
            else
            {
                MessageBox.Show("No matching doctor found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridRemoveDoctor.DataSource = null;
            }
        }

        private void DoctorRemoveBtn_Click(object sender, EventArgs e)
        {
            if (GridRemoveDoctor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedDoctorId = Convert.ToInt64(GridRemoveDoctor.SelectedRows[0].Cells["ID"].Value);

            DialogResult dialogResult = MessageBox.Show(
                "The selected doctor will be deleted.",
                "Confirmation",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
                DialogResult permanentDelete = MessageBox.Show(
                    "Do you want to permanently delete this record?",
                    "Permanently Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                bool isPermanent = permanentDelete == DialogResult.Yes;

                try
                {
                    dbConnection.RemoveDoctor(selectedDoctorId, isPermanent);
                    MessageBox.Show("Doctor removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LastNRemoveDoctor.Text = "";
                    FirstNRemoveDoctor.Text = "";
                    GridRemoveDoctor.DataSource = null;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("FK"))
                    {
                        MessageBox.Show($"Can't remove the data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }




        private void SearchRemoveDepartment_Click(object sender, EventArgs e)
        {
            string name = txtDepartmentRemove.Text;

            DataTable departments = SearchDatabase("department", name);

            if (departments.Rows.Count > 0)
            {
                GridRemoveDepartment.DataSource = departments;
            }
            else
            {
                MessageBox.Show("No matching department found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridRemoveDepartment.DataSource = null;
            }
        }

        private void DepartmentRemoveBtn_Click(object sender, EventArgs e)
        {
            if (GridRemoveDepartment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a department to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedDepartmentId = Convert.ToInt64(GridRemoveDepartment.SelectedRows[0].Cells["ID"].Value);

            DialogResult dialogResult = MessageBox.Show(
                "The selected department will be deleted.",
                "Confirmation",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    dbConnection.RemoveDepartment(selectedDepartmentId);
                    MessageBox.Show("Department removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDepartmentRemove.Text = "";
                    GridRemoveDepartment.DataSource = null;

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("FK"))
                    {
                        MessageBox.Show($"Can't remove the data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

        }



        private void SearchAppointmentsBtn_Click(object sender, EventArgs e)
        {
            string patFirst = PatientFirstAppM.Text;
            string patLast = PatientLastAppM.Text;
            string docFirst = DoctorFirstAppM.Text;
            string docLast = DoctorLastAppM.Text;

            GridAppointments.DataSource = dbConnection.SearchAppointments("Upcoming", docFirst, docLast, patFirst, patLast);
        }

        private void RemoveAppointmentBtn_Click(object sender, EventArgs e)
        {
            if (GridRemoveAppointment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedAppointmentId = Convert.ToInt64(GridRemoveAppointment.SelectedRows[0].Cells["ID"].Value);

            DialogResult dialogResult = MessageBox.Show(
                "The selected appointment will be deleted.",
                "Confirmation",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    dbConnection.RemoveAppointment(selectedAppointmentId);
                    MessageBox.Show("Appointment removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("FK"))
                    {
                        MessageBox.Show($"Can't remove the data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }



        private void RemoveMedicine_Click(object sender, EventArgs e)
        {
            if (GridAppointmentCompleted.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an appointment first!");
                return;

            }

            if (GridMedicins.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a medicine!");
                return;

            }

            Int64 selectedMedId = Convert.ToInt64(GridMedicins.SelectedRows[0].Cells["ID"].Value);


            try
            {
                dbConnection.RemoveMedicine(selectedMedId);
                GridMedicins.DataSource = dbConnection.SearchMedicine(Convert.ToInt64(GridAppointmentCompleted.SelectedRows[0].Cells["ID"].Value));
                MessageBox.Show("Medicine record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMedicineName.Text = "";
                medicineDosage.Text = "";
                medicineDescription.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }




        private void RemoveSpecializationBtn_Click(object sender, EventArgs e)
        {
            if (GridEditDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a doctor first!");
                return;
            }
            if (GridSpeciaization.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a specialization first!");
                return;
            }

            Int64 selectedSpecialId = Convert.ToInt64(GridSpeciaization.SelectedRows[0].Cells["ID"].Value);
            Int64 selectedId = Convert.ToInt64(GridEditDoctors.SelectedRows[0].Cells["ID"].Value);

            try
            {
                dbConnection.DeleteSpecialization(selectedSpecialId);
                MessageBox.Show("Specializations deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEditDoctorSpecialization.Clear();
                GridSpeciaization.DataSource = SearchDatabase("specialization", selectedId.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        #endregion


        #region Update Record

        private void RowSelectionAppointment(object sender, DataGridViewCellEventArgs e)
        {
            if (GridAppointments.SelectedRows.Count != 0)
            {
                DataGridViewRow selectedRow = GridAppointments.SelectedRows[0];

                string status = selectedRow.Cells["Status"].Value?.ToString();
                DateTime dateTime = Convert.ToDateTime(selectedRow.Cells["Date"].Value);
                string description = selectedRow.Cells["Description"].Value?.ToString();

                DateTime date = dateTime.Date;
                DateTime time = dateTime;

                StatusAppointmentM.Text = status;
                timeAppointmentM.Value = time;
                dateAppointmentM.Value = date;
                descrAppointmentM.Text = description;
            }

        }

        private void StatusComboAppointmentChange(object sender, EventArgs e)
        {
            if (StatusAppointmentM.Text == "Canceled" || StatusAppointmentM.Text == "Completed")
            {
                timeAppointmentM.Enabled = false;
                dateAppointmentM.Enabled = false;
            }
            else
            {
                timeAppointmentM.Enabled = true;
                dateAppointmentM.Enabled = true;
            }

        }

        private void UpdateAppointment_Click(object sender, EventArgs e)
        {
            string Status = StatusAppointmentM.Text;

            if (!new[] { "Upcoming", "Canceled", "Completed" }.Contains(Status))
            {
                MessageBox.Show("Invalid Status!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Status == "Canceled" || Status == "Completed")
                {
                    DataGridViewRow selectedRow = GridAppointments.SelectedRows[0];
                    Int64 id = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                    string description = descrAppointmentM.Text;

                    try
                    {
                        dbConnection.UpdateAppointment(id, Status, description);
                        MessageBox.Show("Appointment updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridAppointments.DataSource = null;
                        DoctorFirstAppM.Text = "";
                        DoctorLastAppM.Text = "";
                        PatientFirstAppM.Text = "";
                        PatientLastAppM.Text = "";
                        descrAppointmentM.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    DataGridViewRow selectedRow = GridAppointments.SelectedRows[0];
                    Int64 id = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                    string description = descrAppointmentM.Text;
                    DateTime date = dateAppointmentM.Value;
                    DateTime time = timeAppointmentM.Value;
                    DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);

                    try
                    {
                        dbConnection.UpdateAppointment(id, Status, description, dateTime);
                        MessageBox.Show("Appointment updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridAppointments.DataSource = null;
                        DoctorFirstAppM.Text = "";
                        DoctorLastAppM.Text = "";
                        PatientFirstAppM.Text = "";
                        PatientLastAppM.Text = "";
                        descrAppointmentM.Text = "";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }




        private void SearchPrescriptionBtn_Click(object sender, EventArgs e)
        {
            string patFirst = txtPatientFirstApp.Text;
            string patLast = txtPatientLastApp.Text;
            string docFirst = txtDoctorFirstApp.Text;
            string docLast = txtDoctorLastApp.Text;



            DataTable appointmens = dbConnection.SearchAppointments("Completed", docFirst, docLast, patFirst, patLast);

            if (appointmens.Rows.Count > 0)
            {
                GridAppointmentCompleted.DataSource = appointmens;
            }
            else
            {
                MessageBox.Show("No matching completed appointment found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridAppointmentCompleted.DataSource = null;
            }
        }

        private void AppointmentRowChangedPres(object sender, DataGridViewCellEventArgs e)
        {
            if (GridAppointmentCompleted.SelectedRows.Count != 0)
            {
                GridMedicins.DataSource = dbConnection.SearchMedicine(Convert.ToInt64(GridAppointmentCompleted.SelectedRows[0].Cells["ID"].Value));

                DateTime d = Convert.ToDateTime(GridAppointmentCompleted.SelectedRows[0].Cells["Date"].Value);
                medicineStart.MinDate = d.Date;
                medicineStart.Value = d.Date;
                medicineEnd.MinDate = d.Date;
                medicineEnd.Value = d.Date;
            }
        }

        private void MedicineRowChange(object sender, DataGridViewCellEventArgs e)
        {
            if (GridMedicins.SelectedRows.Count != 0)
            {
                DataGridViewRow selectedRow = GridMedicins.SelectedRows[0];

                txtMedicineName.Text = selectedRow.Cells["Name"].Value.ToString();
                medicineStart.Value = Convert.ToDateTime(selectedRow.Cells["Start_Date"].Value);
                medicineEnd.Value = Convert.ToDateTime(selectedRow.Cells["End_Date"].Value);
                medicineDosage.Text = selectedRow.Cells["Dosage"].Value.ToString();
                medicineDescription.Text = selectedRow.Cells["Description"].Value.ToString();

            }
        }

        private void EditMedicine_Click(object sender, EventArgs e)
        {
            if (GridAppointmentCompleted.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an appointment first!");
                return;

            }

            if (GridMedicins.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a medicine!");
                return;

            }

            Int64 selectedMedId = Convert.ToInt64(GridMedicins.SelectedRows[0].Cells["ID"].Value);

            string name = txtMedicineName.Text;
            string dosage = medicineDosage.Text;
            DateTime start = medicineStart.Value;
            DateTime end = medicineEnd.Value;
            string desc = medicineDescription.Text;

            try
            {
                dbConnection.UpdateMedicine(selectedMedId, name, start, end, dosage, desc);
                GridMedicins.DataSource = dbConnection.SearchMedicine(Convert.ToInt64(GridAppointmentCompleted.SelectedRows[0].Cells["ID"].Value));
                MessageBox.Show("Medicine record updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMedicineName.Text = "";
                medicineDosage.Text = "";
                medicineDescription.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }




        private void SearchBtnEditPatient_Click(object sender, EventArgs e)
        {
            string first = txtEditPatientF.Text;
            string last = txtEditPatientL.Text;

            DataTable patients = SearchDatabase("patients", first, last, false);

            if (patients.Rows.Count > 0)
            {
                GridEditPatient.DataSource = patients;
            }
            else
            {
                MessageBox.Show("No matching patient found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridEditPatient.DataSource = null;
            }
        }

        private void EditPatientRowChange(object sender, DataGridViewCellEventArgs e)
        {
            if (GridEditPatient.SelectedRows.Count != 0)
            {
                DataGridViewRow selectedRow = GridEditPatient.SelectedRows[0];

                editPatientFirstName.Text = selectedRow.Cells["First_Name"].Value.ToString();
                editPatientLastName.Text = selectedRow.Cells["Last_Name"].Value.ToString();
                editPatientGender.Text = selectedRow.Cells["Gender"].Value.ToString();
                editPatientBlood.Text = selectedRow.Cells["Blood_Type"].Value.ToString();
                editPatientPhone.Text = selectedRow.Cells["Phone"].Value.ToString();
                editPatientContact.Text = selectedRow.Cells["Contact"].Value.ToString();
                editPatientBirthday.Value = Convert.ToDateTime(selectedRow.Cells["Birthday"].Value);

            }
        }

        private void UpdatePatientBtn_Click(object sender, EventArgs e)
        {
            if (GridEditPatient.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a patient first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedId = Convert.ToInt64(GridEditPatient.SelectedRows[0].Cells["ID"].Value);
            string firstName = editPatientFirstName.Text;
            string lastName = editPatientLastName.Text;
            string gender = editPatientGender.Text;
            string bloodType = editPatientBlood.Text;
            string phone = editPatientPhone.Text;
            string contact = editPatientContact.Text;
            DateTime birthday = editPatientBirthday.Value;


            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(gender) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(bloodType))
            {
                MessageBox.Show("Please fill all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (!StringValidation(firstName))
            {
                MessageBox.Show("First name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editPatientFirstName.Text = "";
            }
            else if (!StringValidation(lastName))
            {
                MessageBox.Show("Last name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editPatientLastName.Text = "";
            }
            else if (gender != "Male" && gender != "Female" && gender != "Other")
            {
                MessageBox.Show("Invalid gender!");
                editPatientGender.Text = "";
            }
            else if (!PhoneValidation(phone))
            {
                MessageBox.Show("Phone number must be 11 digits containing only numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editPatientPhone.Text = "";
            }
            else if (!PhoneValidation(contact))
            {
                MessageBox.Show("Contact number must be 11 digits!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editPatientContact.Text = "";
            }
            else if (!new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" }.Contains(bloodType))
            {
                MessageBox.Show("Invalid blood type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editPatientBlood.Text = "";
            }
            else
            {
                try
                {
                    dbConnection.UpdatePatient(selectedId, firstName, lastName, gender, bloodType, phone, contact, birthday);
                    MessageBox.Show("Patient record updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEditPatientF.Text = "";
                    txtEditPatientL.Text = "";
                    GridEditPatient.DataSource = null;
                    editPatientFirstName.Clear();
                    editPatientLastName.Clear();
                    editPatientPhone.Clear();
                    editPatientGender.Text = "";
                    editPatientBlood.Text = "";
                    editPatientContact.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void SearchEditDoctorBtn_Click(object sender, EventArgs e)
        {
            string first = txtEditDoctorFirstN.Text;
            string last = txtEditDoctorLastN.Text;

            DataTable doctors = dbConnection.SearchDoctor(first, last, false);

            if (doctors.Rows.Count > 0)
            {
                GridEditDoctors.DataSource = doctors;
            }
            else
            {
                MessageBox.Show("No matching doctor found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridEditDoctors.DataSource = null;
            }

        }

        private void EditDoctorRowChange(object sender, DataGridViewCellEventArgs e)
        {
            if (GridEditDoctors.SelectedRows.Count != 0)
            {
                DataGridViewRow selectedRow = GridEditDoctors.SelectedRows[0];

                Int64 selectedId = Convert.ToInt64(selectedRow.Cells["ID"].Value);
                Int64 departmentId = Convert.ToInt64(selectedRow.Cells["Department_ID"].Value);

                GridSpeciaization.DataSource = SearchDatabase("specialization", selectedId.ToString());
                editDoctorFirst.Text = selectedRow.Cells["First_Name"].Value.ToString();
                editDoctorLast.Text = selectedRow.Cells["Last_Name"].Value.ToString();
                editDoctorPhone.Text = selectedRow.Cells["Phone"].Value.ToString();
                editDoctorDepartments.Text = dbConnection.GetDepartmentNames(departmentId)[0];
            }
        }

        private void UpdateDoctorBtn_Click(object sender, EventArgs e)
        {
            if (GridEditDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a doctor first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedId = Convert.ToInt64(GridEditDoctors.SelectedRows[0].Cells["ID"].Value);

            string first = editDoctorFirst.Text;
            string last = editDoctorLast.Text;
            string phone = editDoctorPhone.Text;
            string department = editDoctorDepartments.Text;

            if (string.IsNullOrWhiteSpace(first) ||
               string.IsNullOrWhiteSpace(last) ||
               string.IsNullOrWhiteSpace(phone) ||
               string.IsNullOrWhiteSpace(department))
            {
                MessageBox.Show("Please fill all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (!StringValidation(first))
            {
                MessageBox.Show("First name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editDoctorFirst.Text = "";
            }
            else if (!StringValidation(last))
            {
                MessageBox.Show("Last name should only contain letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editDoctorLast.Text = "";
            }
            else if (!PhoneValidation(phone))
            {
                MessageBox.Show("Phone number must be 11 digits!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editDoctorPhone.Text = "";
            }
            else if (!editDoctorDepartments.Items.Contains(department))
            {
                MessageBox.Show("Invalid Department!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editDoctorDepartments.Text = "";
            }
            else
            {
                try
                {
                    dbConnection.UpdateDoctor(selectedId, first, last, phone, department);
                    MessageBox.Show("Record updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    editDoctorDepartments.Text = "";
                    editDoctorFirst.Text = "";
                    editDoctorLast.Text = "";
                    editDoctorPhone.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void SpecializationRowChange(object sender, DataGridViewCellEventArgs e)
        {
            if (GridSpeciaization.SelectedRows.Count != 0)
            {
                txtEditDoctorSpecialization.Text = GridSpeciaization.SelectedRows[0].Cells["Name"].Value.ToString();
            }
        }


        private void UpdateSpecializationBtn_Click(object sender, EventArgs e)
        {
            if (GridEditDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a doctor first!");
                return;
            }

            if (GridSpeciaization.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a specialization first!");
                return;
            }

            Int64 selectedSpecialId = Convert.ToInt64(GridSpeciaization.SelectedRows[0].Cells["ID"].Value);
            string Specialization = txtEditDoctorSpecialization.Text;

            if (!StringValidation(Specialization))
            {
                MessageBox.Show("Invalid Specialization", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Int64 selectedId = Convert.ToInt64(GridEditDoctors.SelectedRows[0].Cells["ID"].Value);

                try
                {
                    dbConnection.UpdateSpecialization(selectedSpecialId, Specialization);
                    MessageBox.Show("Specialization updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEditDoctorSpecialization.Clear();
                    GridSpeciaization.DataSource = SearchDatabase("specialization", selectedId.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }



        }



        private void EditDepartmentRowChange(object sender, DataGridViewCellEventArgs e)
        {
            if (GridEditDepartment.SelectedRows.Count != 0)
            {

                txtEditDepartment.Text = GridEditDepartment.SelectedRows[0].Cells["Name"].Value.ToString();
                txtEditDepartment.Enabled = true;
            }
        }

        private void UpdateDepartmentBtn_Click(object sender, EventArgs e)
        {
            if (GridEditDepartment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a department first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedDepartmentId = Convert.ToInt64(GridEditDepartment.SelectedRows[0].Cells["ID"].Value);
            string name = txtEditDepartment.Text;

            if (!StringValidation(name))
            {
                MessageBox.Show("Invalid Department!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    dbConnection.UpdateDepartment(selectedDepartmentId, name);
                    MessageBox.Show("Department updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEditDepartment.Text = "";
                    GridEditDepartment.DataSource = SearchDatabase("department");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }




        private void PaymentSearch_Click(object sender, EventArgs e)
        {
            string first = txtNamePayment.Text;
            string last = txtLastPayment.Text;

            DataTable patient = SearchDatabase("patients", first, last, false);

            if (patient.Rows.Count > 0)
            {
                GridPatientsPayment.DataSource = patient;
            }
            else
            {
                MessageBox.Show("No matching patient found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GridPatientsPayment.DataSource = null;
            }

        }

        private void GridPaymentPatientRowChange(object sender, DataGridViewCellEventArgs e)
        {
            if (GridPatientsPayment.SelectedRows.Count != 0)
            {

                Int64 selectedId = Convert.ToInt64(GridPatientsPayment.SelectedRows[0].Cells["ID"].Value);

                try
                {
                    string desc;
                    string status;
                    DateTime dateTime;
                    long amount;

                    dbConnection.SearchPayment(selectedId, out status, out desc, out amount, out dateTime);

                    paymentAmount.Text = amount.ToString();
                    paymentStatus.Text = status;
                    DateIssuedPayment.Text = dateTime.ToString();
                    paymentDescription.Text = desc;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void StatusPaymentChange(object sender, EventArgs e)
        {

            if (paymentStatus.Text == "Pending")
            {
                paymentStatus.Text = "On Hold";
            }
            else if (paymentStatus.Text == "Completed")
            {
                paymentAmount.Text = "0";
                paymentAmount.Enabled = false;
            }
            else if (paymentStatus.Text == "Awaiting" && paymentAmount.Text == "0")
            {
                paymentAmount.Text = "1";
                paymentAmount.Enabled = true;
            }
            else if (paymentStatus.Text == "On Hold" && paymentAmount.Text == "0")
            {
                paymentAmount.Enabled = true;
            }
            else
            {
                if (paymentAmount.Text == "0")
                {
                    paymentStatus.Text = "Completed";
                    paymentAmount.Enabled = false;
                }
                else
                {
                    paymentAmount.Enabled = true;

                }
            }
        }

        private void AmountChange(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
                MessageBox.Show("Only digits are allowed.");
            }
        }


        private void editPaymentBtn_Click(object sender, EventArgs e)
        {
            if (GridPatientsPayment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a patient first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 selectedPatientId = Convert.ToInt64(GridPatientsPayment.SelectedRows[0].Cells["ID"].Value);

            string status = paymentStatus.Text;
            string amount = paymentAmount.Text;
            DateTime date = DateTime.Now;
            string desc = paymentDescription.Text;

            if (string.IsNullOrWhiteSpace(status) && amount == "")
            {
                MessageBox.Show("Please fill all the fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    dbConnection.UpdatePayment(selectedPatientId, status, Convert.ToInt64(amount), date, desc);
                    MessageBox.Show("Payment updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNamePayment.Text = "";
                    txtLastPayment.Text = "";
                    paymentDescription.Text = "";
                    paymentAmount.Clear();
                    GridPatientsPayment.DataSource = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        #endregion


        #region Other Functions

        private void DateCorrector()
        {
            dateAddApp.MinDate = DateTime.Now;
            BirthAddPatient.MaxDate = DateTime.Now;
            editPatientBirthday.MaxDate = DateTime.Now;
        }

        private bool StringValidation(string value)
        {
            return Regex.IsMatch(value, @"^[a-z A-Z]+$") && value.Length > 2;
        }

        private bool PhoneValidation(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{11}$");
        }



        private DataTable SearchDatabase(string table, string first = "", string last = "", bool Deleted = true)
        {
            DataTable res = new DataTable();

            switch (table)
            {
                case "patients":
                    res = dbConnection.SearchPatient(first, last, Deleted);
                    break;
                case "doctors":
                    res = dbConnection.SearchDoctor(first, last, Deleted);
                    break;
                case "department":
                    res = dbConnection.SearchDepartmentRemove(first);
                    break;
                case "appointment":
                    break;
                case "specialization":
                    res = dbConnection.SearchSpecialization(Convert.ToInt64(first));
                    break;
            }


            return res;
        }



        #endregion




    }
}
