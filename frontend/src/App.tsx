import React from 'react'
import './index.css'
import LoginPage from './components/Page/LoginPage'
import HomePage from './components/Page/HomePage'
import DoctorMain from './components/Doctor/DoctorMain'
import PatientAwaitDetail from './components/Doctor/PatientAwaitDetail'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from './components/layout/Navbar'
const App = () => {
  return (
    <BrowserRouter>
    <Navbar/>
    <Routes>
        < Route path='/' element={<DoctorMain/>}/>
        <Route path='/detail/:patient_id' element={< PatientAwaitDetail/>}/>
    </Routes>
    </BrowserRouter>
   
  )
}
export default App


