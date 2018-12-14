namespace CarRentalWinFormClient
{
    partial class CarAdministrationAddForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblRegNr = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblAddNewCar = new System.Windows.Forms.Label();
            this.lblManYear = new System.Windows.Forms.Label();
            this.txtRegNr = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtManYear = new System.Windows.Forms.TextBox();
            this.btnSaveCar = new System.Windows.Forms.Button();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.lblCarAdministration = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrand.Location = new System.Drawing.Point(17, 80);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(59, 20);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "Brand";
            // 
            // lblRegNr
            // 
            this.lblRegNr.AutoSize = true;
            this.lblRegNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegNr.Location = new System.Drawing.Point(17, 162);
            this.lblRegNr.Name = "lblRegNr";
            this.lblRegNr.Size = new System.Drawing.Size(182, 20);
            this.lblRegNr.TabIndex = 2;
            this.lblRegNr.Text = "Registration Number";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(17, 121);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(59, 20);
            this.lblModel.TabIndex = 3;
            this.lblModel.Text = "Model";
            // 
            // lblAddNewCar
            // 
            this.lblAddNewCar.AutoSize = true;
            this.lblAddNewCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddNewCar.Location = new System.Drawing.Point(15, 45);
            this.lblAddNewCar.Name = "lblAddNewCar";
            this.lblAddNewCar.Size = new System.Drawing.Size(187, 25);
            this.lblAddNewCar.TabIndex = 4;
            this.lblAddNewCar.Text = "Add a new car here:";
            // 
            // lblManYear
            // 
            this.lblManYear.AutoSize = true;
            this.lblManYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManYear.Location = new System.Drawing.Point(20, 201);
            this.lblManYear.Name = "lblManYear";
            this.lblManYear.Size = new System.Drawing.Size(170, 20);
            this.lblManYear.TabIndex = 5;
            this.lblManYear.Text = "Manufacturing year";
            // 
            // txtRegNr
            // 
            this.txtRegNr.Location = new System.Drawing.Point(237, 162);
            this.txtRegNr.Name = "txtRegNr";
            this.txtRegNr.Size = new System.Drawing.Size(232, 22);
            this.txtRegNr.TabIndex = 7;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(237, 121);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(232, 22);
            this.txtModel.TabIndex = 8;
            // 
            // txtManYear
            // 
            this.txtManYear.Location = new System.Drawing.Point(237, 198);
            this.txtManYear.Name = "txtManYear";
            this.txtManYear.Size = new System.Drawing.Size(232, 22);
            this.txtManYear.TabIndex = 9;
            // 
            // btnSaveCar
            // 
            this.btnSaveCar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSaveCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCar.ForeColor = System.Drawing.Color.Black;
            this.btnSaveCar.Location = new System.Drawing.Point(343, 253);
            this.btnSaveCar.Name = "btnSaveCar";
            this.btnSaveCar.Size = new System.Drawing.Size(117, 47);
            this.btnSaveCar.TabIndex = 10;
            this.btnSaveCar.Text = "Save car";
            this.btnSaveCar.UseVisualStyleBackColor = false;
            this.btnSaveCar.Click += new System.EventHandler(this.btnSaveCar_Click);
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(237, 80);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(232, 22);
            this.txtBrand.TabIndex = 6;
            // 
            // lblCarAdministration
            // 
            this.lblCarAdministration.AutoSize = true;
            this.lblCarAdministration.Font = new System.Drawing.Font("Noto Mono", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarAdministration.Location = new System.Drawing.Point(208, 13);
            this.lblCarAdministration.Name = "lblCarAdministration";
            this.lblCarAdministration.Size = new System.Drawing.Size(339, 33);
            this.lblCarAdministration.TabIndex = 0;
            this.lblCarAdministration.Text = "Car Administration";
            // 
            // CarServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 720);
            this.Controls.Add(this.btnSaveCar);
            this.Controls.Add(this.txtManYear);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtRegNr);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.lblManYear);
            this.Controls.Add(this.lblAddNewCar);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblRegNr);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lblCarAdministration);
            this.Name = "CarServiceForm";
            this.Text = "CarServiceForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblRegNr;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblAddNewCar;
        private System.Windows.Forms.Label lblManYear;
        private System.Windows.Forms.TextBox txtRegNr;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtManYear;
        private System.Windows.Forms.Button btnSaveCar;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label lblCarAdministration;
    }
}