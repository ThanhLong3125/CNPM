import './index.css'
import { BrowserRouter, Routes, Route} from "react-router-dom";
import DetailCreatedRecord from './components/ReceptionStaff/MedicalRecordForm';
import LoginPage from './components/Page/LoginPage'
import DoctorMain from './components/Doctor/DoctorMain'
import StaffLayout from './components/layout/StaffLayout'
import MainStaff from "./components/ReceptionStaff/MainStaff"
import CreatedRecord from "./components/ReceptionStaff/CreatedRecordList"
import NewRecord from "./components/ReceptionStaff/NewRecord"
import PatientRecordView from './components/ReceptionStaff/PatientRecord'
import HistoryRecords from "./components/ReceptionStaff/History_records"
import CreateMedicalRecord from "./components/ReceptionStaff/CreateMedicalRecord"
import UpdatePatientRecord from './components/ReceptionStaff/UpdatePatientRecord'
import ReceptionProfile from './components/ReceptionStaff/ReceptionProfile'
import DoctorLayout from './components/layout/DoctorLayout';
import EditMedicalRecord from "../src/components/ReceptionStaff/EditMedicalRecord"
import HomePage from './components/Page/HomePage';
import MedicalRecord from './components/Doctor/MedicalRecord';
import History_records from './components/ReceptionStaff/History_records';
import OldMedicalRecord from './components/Doctor/OldMedicalRecord';

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={< HomePage />} />
        <Route path="/login" element={<LoginPage />} />

        <Route path="/staff" element={<StaffLayout />}>
          <Route index element={<MainStaff />} />
          <Route path="mainstaff" element={<MainStaff />} />
          <Route path="CreatedRecordList" element={<CreatedRecord />} />
          <Route path="create-profile" element={<NewRecord />} />
          <Route path="patientRecord/:patientId" element={<PatientRecordView />} />
          <Route path="medical-history/:patientId" element={<HistoryRecords />} />
          <Route path="create-medical-record/:patientId" element={<CreateMedicalRecord />} />
          <Route path="update-patient/:patientId" element={<UpdatePatientRecord />} />
          <Route path="user-profile" element={<ReceptionProfile />} />
          <Route path="DetailCreatedRecord/:medicalRecordId" element={<DetailCreatedRecord/>} />
          <Route path="EditMedicalRecord/:medicalRecordId" element={<EditMedicalRecord/>} />
        </Route>

        <Route path="/doctor" element={<DoctorLayout />}>
          <Route index element={<DoctorMain />} />
          <Route path="doctormain" element={<DoctorMain/>} />
          <Route path="MedicalRecord/:patient_id" element={<MedicalRecord />} />
          <Route path="medical-history/:patientId" element={<History_records/>} />
          <Route path="OldMedicalRecord/:patientId" element={<OldMedicalRecord/>} />
        </Route>

      </Routes>
    </BrowserRouter>
  )
}

export default App
