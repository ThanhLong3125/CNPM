import React from "react";
import './index.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";

import LoginPage from './components/Page/LoginPage';
import HomePage from './components/Page/HomePage';
import DoctorMain from './components/Doctor/DoctorMain';
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail';
import CreateMedicalRecord from './components/Staff/CreateMedicalRecord';
import MedicalRecordForm from './components/Staff/MedicalRecordForm'
import EditMedicalRecord from './components/Staff/EditMedicalRecord'
import Navbar from './components/layout/Navbar';

const App = () => {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/" element={<EditMedicalRecord />} />
        {/* <Route path="/login" element={<LoginPage />} />
        <Route path="/home" element={<HomePage />} /> */}
      </Routes>
    </BrowserRouter>
  );
};

export default App;
