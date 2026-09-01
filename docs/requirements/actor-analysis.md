# Hospital Enterprise Platform — Actor Analysis

## 1. Receptionist

### 1.1 Responsibilities

The Receptionist is responsible for:

* Registering new patients.
* Searching for existing patients.
* Updating patient information.
* Deactivating patient records when required.
* Scheduling appointments.
* Cancelling appointments.
* Viewing appointment schedules.
* Providing administrative patient information required for reception activities.

### 1.2 Commands

The Receptionist may perform commands such as:

```text
patient add
patient search
patient list
patient update
patient deactivate

appointment create
appointment list
appointment cancel
```

### 1.3 Information They Can See

The Receptionist can access:

* Patient identification information.
* Patient contact information.
* Patient appointment information.
* Doctor availability and schedules.
* Basic department information.
* Patient active/inactive status.

### 1.4 Information They Cannot See

The Receptionist should not access:

* Detailed medical diagnoses.
* Detailed prescriptions.
* Sensitive medical information unless specifically authorized.
* Full audit logs.
* Administrative configuration.
* Plugin management information.
* Passwords or authentication credentials.
* Internal system implementation details.

### 1.5 Modules

The Receptionist interacts primarily with:

* Authentication & Authorization
* Patient Management
* Appointment Management
* Department Management

---

# 2. Doctor

### 2.1 Responsibilities

The Doctor is responsible for:

* Viewing assigned appointments.
* Viewing relevant patient information.
* Reviewing patient medical history.
* Recording diagnoses.
* Creating prescriptions.
* Requesting laboratory tests.
* Reviewing laboratory results.
* Completing appointments.

### 2.2 Commands

The Doctor may perform commands such as:

```text
appointment list --doctor D001
patient search
medical-record view
diagnosis add
prescription add
lab request
lab result view
appointment complete
```

### 2.3 Information They Can See

The Doctor can access:

* Patient identification information.
* Relevant contact information.
* Appointment information.
* Medical history.
* Diagnoses.
* Prescriptions.
* Laboratory requests.
* Laboratory results.
* Relevant admission information.
* Relevant patient vitals.

### 2.4 Information They Cannot See

The Doctor should not access:

* Patient passwords or authentication credentials.
* Full administrative configuration.
* Plugin management controls.
* Other users' credentials.
* Sensitive administrative information unrelated to patient care.
* Full financial information unless required by an authorized workflow.

### 2.5 Modules

The Doctor interacts primarily with:

* Authentication & Authorization
* Patient Management
* Appointment Management
* Medical Records
* Laboratory
* Admissions
* Department Management

---

# 3. Nurse

### 3.1 Responsibilities

The Nurse is responsible for:

* Accessing relevant patient information.
* Supporting patient admissions.
* Recording patient vitals.
* Viewing assigned patients.
* Supporting inpatient care.
* Working with assigned departments.

### 3.2 Commands

The Nurse may perform commands such as:

```text
patient search
patient view
admission list
admission view
vitals add
vitals list
department view
```

### 3.3 Information They Can See

The Nurse can access:

* Patient identification information.
* Relevant contact information.
* Assigned patient information.
* Admission information.
* Ward and bed information.
* Patient vitals.
* Relevant medical information required for patient care.
* Appointment information when relevant.

### 3.4 Information They Cannot See

The Nurse should not access:

* Passwords.
* Other users' credentials.
* Full administrative configuration.
* Plugin management controls.
* Billing administration information unless required.
* Sensitive information unrelated to their assigned responsibilities.

### 3.5 Modules

The Nurse interacts primarily with:

* Authentication & Authorization
* Patient Management
* Admissions & Beds
* Medical Records
* Departments
* Appointments

---

# 4. Lab Technician

### 4.1 Responsibilities

The Lab Technician is responsible for:

* Receiving laboratory requests.
* Processing laboratory requests.
* Processing emergency requests.
* Recording laboratory results.
* Completing laboratory requests.
* Cancelling laboratory requests when appropriate.

### 4.2 Commands

The Lab Technician may perform commands such as:

```text
lab request list
lab request view
lab request process
lab result add
lab request complete
lab request cancel
```

### 4.3 Information They Can See

The Lab Technician can access:

* Patient identification information required for testing.
* Laboratory requests.
* Test types.
* Laboratory queue information.
* Emergency request priority.
* Laboratory results.
* Relevant information required to perform the requested test.

### 4.4 Information They Cannot See

The Lab Technician should not access:

* Patient passwords.
* Billing details unrelated to laboratory work.
* Full insurance information.
* Administrative configuration.
* Plugin management controls.
* Unrelated medical information unless required for the laboratory procedure.

### 4.5 Modules

The Lab Technician interacts primarily with:

* Authentication & Authorization
* Laboratory Management
* Patient Management
* Notifications

---

# 5. Pharmacist

### 5.1 Responsibilities

The Pharmacist is responsible for:

* Reviewing prescriptions.
* Checking medication availability.
* Dispensing medications.
* Updating medication stock.
* Monitoring low-stock medications.
* Monitoring medication expiration.
* Handling medication-related notifications.

### 5.2 Commands

The Pharmacist may perform commands such as:

```text
prescription list
prescription view
pharmacy medication list
pharmacy stock
pharmacy dispense
pharmacy expiring
```

### 5.3 Information They Can See

The Pharmacist can access:

* Prescription information required for dispensing.
* Medication names.
* Dosage.
* Duration.
* Medication stock.
* Unit prices where required.
* Expiration dates.
* Low-stock information.
* Relevant patient identification information.

### 5.4 Information They Cannot See

The Pharmacist should not access:

* Patient passwords.
* Full medical history unless required for medication safety.
* Administrative configuration.
* Plugin management controls.
* Unrelated billing information.
* Other users' credentials.

### 5.5 Modules

The Pharmacist interacts primarily with:

* Authentication & Authorization
* Pharmacy & Inventory
* Medical Records
* Patient Management
* Notifications

---

# 6. Billing Clerk

### 6.1 Responsibilities

The Billing Clerk is responsible for:

* Creating invoices.
* Adding hospital services.
* Adding applicable medication costs.
* Adding laboratory fees.
* Calculating invoice totals.
* Checking insurance coverage.
* Recording payments.

### 6.2 Commands

The Billing Clerk may perform commands such as:

```text
invoice create
invoice add-line
invoice view
invoice calculate
insurance check
payment record
```

### 6.3 Information They Can See

The Billing Clerk can access:

* Patient identification information required for billing.
* Appointment fees.
* Medication charges.
* Laboratory fees.
* Hospital service charges.
* Insurance coverage information.
* Invoice information.
* Payment information.

### 6.4 Information They Cannot See

The Billing Clerk should not access:

* Detailed medical diagnoses unless required for billing.
* Detailed clinical notes.
* Patient passwords.
* Other users' credentials.
* Plugin management controls.
* Unrelated clinical information.

### 6.5 Modules

The Billing Clerk interacts primarily with:

* Authentication & Authorization
* Billing & Payment
* Insurance Integration
* Patient Management
* Appointment Management
* Pharmacy
* Laboratory

---

# 7. Administrator

### 7.1 Responsibilities

The Administrator is responsible for:

* Managing users.
* Managing roles.
* Managing employees.
* Managing departments.
* Managing system configuration.
* Viewing audit information.
* Managing backups.
* Managing plugins.
* Running administrative commands.
* Accessing management reports.

### 7.2 Commands

The Administrator may perform commands such as:

```text
user create
user list
role assign

employee add
employee list

department add
department tree

audit list
audit search

report generate
backup start
backup restore

plugin list
plugin run
```

### 7.3 Information They Can See

The Administrator can access:

* User information.
* Employee information.
* Department information.
* System configuration.
* Audit records.
* Administrative reports.
* Backup information.
* Plugin information.
* System status.
* Appropriate hospital operational information.

### 7.4 Information They Cannot See

Even the Administrator should not access:

* Plaintext passwords.
* Unmasked sensitive data where masking is required.
* Secret credentials used by external services.

### 7.5 Modules

The Administrator interacts with:

* Authentication & Authorization
* User & Staff Management
* Department Management
* Patient Management
* Appointment Management
* Laboratory
* Pharmacy
* Admissions
* Billing
* Audit
* Reporting
* Backup
* Import & Export
* Plugin Management
* Command Management

---

# 8. Insurance Provider

The Insurance Provider is an **external system**, not a hospital employee.

### 8.1 Purpose

The Insurance Provider is used to determine whether a patient's insurance coverage applies to an invoice or hospital service.

In V1, the service is simulated.

### 8.2 Information Exchanged

The Hospital Enterprise Platform may send:

* Insurance identifier.
* Patient identifier required for verification.
* Invoice/service information required for coverage checking.

The Insurance Provider may return:

* Coverage status.
* Covered amount or coverage information.
* Insurance response status.

Only information required for insurance verification should be exchanged.

### 8.3 Interaction Boundary

```text
Hospital Enterprise Platform
            │
            │ HTTP / Contract
            ↓
     Insurance Provider
       External System
```

The Insurance Provider is outside the V1 system boundary.

The hospital system communicates with it through a defined contract.

If the Insurance Provider is unavailable, billing must continue through the documented fallback/manual-override process.

The external service must not terminate or crash the hospital application.

---

# 9. Medication Supplier

The Medication Supplier is an **external system**, not a hospital employee.

### 9.1 Purpose

The Medication Supplier represents an external service used for medication supply-related operations.

In V1, the service is simulated.

### 9.2 Information Exchanged

The Hospital Enterprise Platform may send:

* Medication identifier.
* Requested medication quantity.
* Supplier-related request information.

The Medication Supplier may return:

* Medication availability.
* Supply information.
* Request status.
* Supplier response information.

### 9.3 Interaction Boundary

```text
Hospital Enterprise Platform
            │
            │ HTTP / Contract
            ↓
     Medication Supplier
       External System
```

The Medication Supplier is outside the V1 system boundary.

The hospital system communicates with it through a defined contract.

Failure of the external service must be handled gracefully and must not crash the main hospital application.
