import React from "react";
import './Student.css'
import { Link } from "react-router-dom";
const Student = (props) =>{
    
    return(
        <div style={{ marginTop: "30px", width:"100%"}}>
            <table id="customers">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Email</th>
                        <th>phone</th>
                        <th>zipCode</th>
                        <th>Hobbies</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                {props.students.map((item, index) => (
                    <tr key={index}>
                        <td>{item.name} </td>
                        <td>{item.email} </td>
                        <td>{item.phone} </td>
                        <td>{item.zipcode} </td>
                        <td>{item.hobbies} </td>
                        <td>
                            <Link className="button" to={`/Student/${item.id}`}>Edit</Link>
                        </td>
                    </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default Student;