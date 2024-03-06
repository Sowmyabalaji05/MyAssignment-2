import { Form, Button, Container, Row, Col, Card } from 'react-bootstrap';
import Header from './Header';
import Footer from './Footer';
import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { useParams,Link } from 'react-router-dom';

const EditEmployee = () => {
    const { employeeId } = useParams();
    
    const [employeeDetails, setEmployeeDetails] = useState({
        employeeId: '',
        employeeName: '',
        email: '',
        managerID: '',
        managerName: '',
        isActive: false,
    });
   
    useEffect(() => {
        if (employeeId) { // Check if employeeId is defined
            fetchEmployeeDetails(employeeId);
            console.log(employeeId);
        }
    },[]);

    const fetchEmployeeDetails = async () => {
        try {
            const response = await axios.get(`https://localhost:7013/api/Employee/${employeeId}`);
            setEmployeeDetails(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching employee details:', error);
        }
    };
    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const response = await axios.put(`https://localhost:7013/api/Employee/${employeeId}`, employeeDetails);
            console.log(response.data);
            alert('Employee details updated successfully!');
        } catch (error) {
            console.error('Error updating employee details:', error);
            alert('Failed to update employee details. Please try again.');
        }
    };

    return (
        <div>
            <Header />
            <Container className="d-flex justify-content-center align-items-center vh-100">
                <Row>
                    <Col md={8} lg={6} className="mb-3">
                        <Card style={{ width: '30rem' }}>
                            <Card.Body>
                                <h3 className="employee-details-header">Edit Employee</h3>
                                <Form onSubmit={handleSubmit}>
                                    <Form.Group as={Row} controlId="employeeId">
                                        <Form.Label column sm="4" className="mb-3">Employee ID</Form.Label>
                                        <Col sm="8">
                                            <Form.Control type="text" placeholder="Enter Employee ID" value={employeeDetails.employeeId} onChange={(e) => setEmployeeDetails({ ...employeeDetails, employeeId: e.target.value })} />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row} controlId="employeeName">
                                        <Form.Label column sm="4" className="mb-3">Employee Name</Form.Label>
                                        <Col sm="8">
                                            <Form.Control type="text" placeholder="Enter Employee Name" value={employeeDetails.employeeName} onChange={(e) => setEmployeeDetails({ ...employeeDetails, employeeName: e.target.value })} />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row} controlId="email">
                                        <Form.Label column sm="4" className="mb-3">Email</Form.Label>
                                        <Col sm="8">
                                            <Form.Control type="email" placeholder="Enter Email" value={employeeDetails.email} onChange={(e) => setEmployeeDetails({ ...employeeDetails, email: e.target.value })} />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row} controlId="managerId">
                                        <Form.Label column sm="4" className="mb-3">Manager ID</Form.Label>
                                        <Col sm="8">
                                            <Form.Control type="text" placeholder="Enter Manager ID" value={employeeDetails.managerID} onChange={(e) => setEmployeeDetails({ ...employeeDetails, managerID: e.target.value })} />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row} controlId="managerName">
                                        <Form.Label column sm="4" className="mb-3">Manager Name</Form.Label>
                                        <Col sm="8">
                                            <Form.Control type="text" placeholder="Enter Manager Name" value={employeeDetails.managerName} onChange={(e) => setEmployeeDetails({ ...employeeDetails, managerName: e.target.value })} />
                                        </Col>
                                    </Form.Group>

                                    <Form.Group as={Row} controlId="isActive" className="mb-3">
                                        <Form.Label column sm="4" className="mb-3">Is Active</Form.Label>
                                        <Col sm="8" className="d-flex align-items-center">
                                            <Form.Check type="checkbox" checked={employeeDetails.isActive} onChange={(e) => setEmployeeDetails({ ...employeeDetails, isActive: e.target.checked })} />
                                        </Col>
                                    </Form.Group>

                                    <Button variant="primary" type="submit">
                                        Save Changes
                                    </Button>
                                    <Link to="/" className="btn btn-secondary ml-2">Back</Link>
                                </Form>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Container>
            <Footer />
        </div>
    );
}

export default EditEmployee;
