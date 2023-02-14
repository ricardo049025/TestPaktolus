import './NewStudent.css';
import React, { useEffect, useState } from "react";
import { useParams, useHistory } from 'react-router-dom';
import Card from "../../shared/components/UIElements/Card";
import Input from '../../shared/components/FormElements/Input';
import Button from "../../shared/components/FormElements/Button";
import { useForm } from "../../shared/components/hooks/form-hook";
import ErrorModal from "../../shared/components/UIElements/ErrorModal";
import { useHttpClient } from "../../shared/components/hooks/http-hook";
import {VALIDATOR_REQUIRE,VALIDATOR_EMAIL, VALIDATOR_MAXLENGTH,VALIDATOR_NUMBER, VALIDATOR_MINLENGTH} from '../../shared/components/utils/validator';

const UpdateStudent = () =>{
    const history = useHistory();
    const [checked, setChecked] = useState([]);
    const [loadedHobbies, setLoadedHobbies] = useState([]);
    const [loadedStudent,setLoadedStudent] = useState();
    const studentId = useParams().studentId;
    const [formState, inputHandler,setFormData] =   useForm({
      name: {
          value: '',
          isValid: false
      },
      email:{
          value: '',
          isValid: false
      },
      phone:{
          value: '',
          isValid: false
      },
      zipcode:{
          value: '',
          isValid: false
      }
      
  },false);

  const {isLoading, error,sendRequest,clearError} = useHttpClient();

    useEffect( () =>{
      const fetchplaces = async () =>{
        try {
          const responseData = await sendRequest(`https://localhost:7222/api/student/${studentId}`);
          setLoadedStudent(responseData);
          setChecked(responseData.hobbies);
          console.log(responseData.hobbies); 

        } catch (err) {}
      };
      fetchplaces();
    }, [sendRequest,studentId])


    useEffect(() => {
      if(loadedStudent){
        setFormData(
          {
            name: {
              value: loadedStudent.name,
              isValid: true
            },
            email: {
              value: loadedStudent.email,
              isValid: true
            },
            phone: {
              value: loadedStudent.phone,
              isValid: true
            },
            zipcode: {
              value: loadedStudent.zipcode,
              isValid: true
            }
          },
          true
        );
      }
      
    }, [setFormData, loadedStudent]);
    
    const updateSubmitHandler = async (event) =>{
        event.preventDefault();

        event.preventDefault();
    
    try {
      await sendRequest(
        `https://localhost:7222/api/student/update/${studentId}`,
        "PATCH",
        JSON.stringify({
          id: studentId,
          name: formState.inputs.name.value,
          email: formState.inputs.email.value,
          phone: formState.inputs.phone.value,
          zipCode: formState.inputs.zipcode.value,
          hobbies: checked
        }),
        {'Content-Type': 'application/json'}
        );

      history.push('/');
    } catch (err) {}

    };
    
    //loading hobbies
    useEffect(() => {
        const fetchUsers = async () => {
          try {
            const responseData = await sendRequest(
              'https://localhost:7222/api/Hobby'
            );
            setLoadedHobbies(responseData); 
          } catch (err) {}
        };
        fetchUsers();
      }, [sendRequest]);
    
      //habler check button
      const handleCheck = (event) => {

        var updatedList = [...checked];
        if (event.target.checked) {
          updatedList = [...checked, parseInt(event.target.id)];
        } else {
          updatedList.splice(checked.indexOf(parseInt(event.target.id)), 1);
        }
         setChecked(updatedList);
         console.log(checked);
      };

    return(
        <React.Fragment>
          <ErrorModal error={error} onClear={clearError} />
          <Card className="authentication">
              <h2>Edit User</h2>
              <hr/>
              {!isLoading && loadedStudent && (
                <form className="place-form" onSubmit={updateSubmitHandler}>
                    <Input
                      id="name"
                      element="input"
                      type="text"
                      label="Name"
                      validators={[VALIDATOR_REQUIRE(),VALIDATOR_MAXLENGTH(50)]}
                      errorText="Field required, has a max number of character 50!!"
                      onInput={inputHandler}
                      initialValue={loadedStudent.name}
                      initialValid={true}
                    />
                    <Input
                    id="email"
                    element="input"
                    type="email"
                    label="E-mail"
                    validators={[VALIDATOR_REQUIRE(), VALIDATOR_EMAIL()]}
                    errorText="Please enter a valid email."
                    onInput={inputHandler}
                    initialValue={loadedStudent.email}
                      initialValid={true}
                    />
                    <Input
                    id="phone"
                    element="input"
                    type="text"
                    label="Phone-Number"
                    validators={[VALIDATOR_REQUIRE(), VALIDATOR_NUMBER(), VALIDATOR_MINLENGTH(10), VALIDATOR_MAXLENGTH(10)]}
                    errorText="Enter only 12 number characters"
                    onInput={inputHandler}
                    initialValue={loadedStudent.phone}
                    initialValid={true}
                    />
                    <Input
                    id="zipCode"
                    element="input"
                    type="text"
                    label="Zip Code"
                    validators={[VALIDATOR_REQUIRE(), VALIDATOR_NUMBER(), VALIDATOR_MAXLENGTH(5)]}
                    errorText="Field required, only numbers"
                    onInput={inputHandler}
                    initialValue={loadedStudent.zipcode}
                    initialValid={true}
                    />
                    <div className="checkList">
                        <div className="title">Hobbies:</div>
                        <div className="list-container">
                        {loadedHobbies && loadedHobbies.map((item, index) => (
                            <div key={index}>
                            <input id={item.id} checked={checked.includes(item.id)} type="checkbox" onChange={(event) => handleCheck(event)}/>
                            <span>{item.name}</span>
                          </div>
                        ))}
                        </div>
                    </div>
                    <div className='center' style={{marginTop:"30px"}}>
                    <Button  type="submit" disabled={!formState.isValid}>SAVE </Button>
                    <Button  type="button" inverse>CANCEL </Button>
                    </div>
                </form>    
              )}
          </Card>
        </React.Fragment>
    )
}

export default UpdateStudent;