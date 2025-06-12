import React from 'react'
import './index.css'
import LoginPage from './components/Page/LoginPage'
import HomePage from './components/Page/HomePage'
import DoctorMain from './components/Doctor/DoctorMain'
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail'
import MedicalRecord from './components/Doctor/MedicalRecord'
import OldMedicalRecord from './components/Doctor/OldMedicalRecord'

import { BrowserRouter, Routes, Route } from "react-router-dom";


const App = () => {
  return (
    // <BrowserRouter>
    // <Routes>
    //     < Route path='/' element={<DoctorMain/>}/>
    //     <Route path='/detail/:patient_id' element={< PatientAwaitDetail/>}/>
    // </Routes>
    // </BrowserRouter>
 
  <MedicalRecord></MedicalRecord>
   
  )
}
export default App





