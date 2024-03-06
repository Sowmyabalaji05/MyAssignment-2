import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import EmployeeList from './components/EmployeeList';
import AddEmployee from './components/AddEmployee';
import EditEmployee from './components/EditEmploye';

 
 
 
function App() {
  return (
    <div className="App">
      
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<EmployeeList />}/>
          <Route path='/employee/addemployee' element={<AddEmployee/>}/>
          <Route path='/employee/edit/:employeeId' element={<EditEmployee />}/>
 
        </Routes>
      </BrowserRouter>
    </div>
  );
 
};
 
export default App;