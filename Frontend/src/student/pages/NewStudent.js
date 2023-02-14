import './NewStudent.css';
import Student from './Student';
import React, { useEffect, useState } from "react";
import Card from "../../shared/components/UIElements/Card";
import Modal from '../../shared/components/UIElements/Modal';
import Input from '../../shared/components/FormElements/Input';
import Button from "../../shared/components/FormElements/Button";
import { useForm } from "../../shared/components/hooks/form-hook";
import ErrorModal from "../../shared/components/UIElements/ErrorModal";
import { useHttpClient } from "../../shared/components/hooks/http-hook";
import LoadingSpinner from '../../shared/components/UIElements/LoadingSpinner';
import {VALIDATOR_REQUIRE,VALIDATOR_EMAIL, VALIDATOR_MAXLENGTH,VALIDATOR_NUMBER, VALIDATOR_MINLENGTH} from '../../shared/components/utils/validator';



const NewStudent = () =>{
    
  const [checked, setChecked] = useState([]);
  const [loadedHobbies, setLoadedHobbies] = useState();
  const [loadedStudent,setLoadedStudent] = useState();
  const [showError, setShowError] = useState(false);
  const showErrorHandler = () => {setShowError(true)};
  const cancelErrorHandler = () => {setShowError(false)};
  const [header, setHeader] = useState('');

    const [formState, inputHandler] =   useForm({
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
        zipCode:{
            value: '',
            isValid: false
        }
        
    },false);
    const {isLoading, error,sendRequest,clearError} = useHttpClient();
    
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
    
    //loading students with hobbies
    useEffect(() => {
      const fetchUsers = async () => {
        try {
          const responseData = await sendRequest(
            'https://localhost:7222/api/Student'
          );
          setLoadedStudent(responseData); 
          console.log(responseData);
        } catch (err) {}
      };
      
      fetchUsers();
      
    }, [sendRequest]);

    //adding new student
    const authSubmitHandler = async (event) =>{
        event.preventDefault();

        let newStu = {name: formState.inputs.name.value,
          email: formState.inputs.email.value,
          phone: formState.inputs.phone.value, 
          zipCode: formState.inputs.zipCode.value, 
          hobbies: checked}

        const responseData = await sendRequest(
            'https://localhost:7222/api/Student',
            'POST',
            JSON.stringify(newStu),
            {'Content-Type': 'application/json'}); 
        
        if(responseData.success){
          newStu = responseData.student;
          const insertedStudent = [...loadedStudent,newStu];
          setLoadedStudent(insertedStudent);
        }
        else{
          setHeader(responseData.xerror);
          setShowError(true);
          //alert(responseData.xerror);
        }
    };

    // checking the  hobbies
    const handleCheck = (event) => {
        var updatedList = [...checked];
        if (event.target.checked) {
          updatedList = [...checked, event.target.id];
        } else {
          updatedList.splice(checked.indexOf(event.target.id), 1);
        }
         setChecked(updatedList);
      };

    const resetHandler = () => {
      
    }

    return (
        <React.Fragment>
            <ErrorModal error={error} onClear={clearError} />
            <Modal show={showError} 
            onCancel={cancelErrorHandler} 
            header="Error message"
            children={header}
            contentClass="place-item__modal-content" 
            footerClass="place-item__modal-actions" 
            footer={<Button onClick={cancelErrorHandler}>CLOSE</Button>}>
            </Modal>
            <Card className="authentication">
                <h2>Students</h2>
                <hr/>
                <form onSubmit={authSubmitHandler} className="place-form">
                {isLoading && <LoadingSpinner asOverlay/>}
                    <Input
                    id="name"
                    element="input"
                    type="text"
                    label="Name"
                    validators={[VALIDATOR_REQUIRE(),VALIDATOR_MAXLENGTH(50)]}
                    errorText="Field required, has a max number of character 50!!"
                    onInput={inputHandler}
                    />
                     <Input
                    id="email"
                    element="input"
                    type="email"
                    label="E-mail"
                    validators={[VALIDATOR_REQUIRE(), VALIDATOR_EMAIL()]}
                    errorText="Please enter a valid email."
                    onInput={inputHandler}
                    />
                    <Input
                    id="phone"
                    element="input"
                    type="text"
                    label="Phone-Number"
                    validators={[VALIDATOR_REQUIRE(), VALIDATOR_NUMBER(), VALIDATOR_MINLENGTH(10), VALIDATOR_MAXLENGTH(10)]}
                    errorText="Enter only 12 number characters"
                    onInput={inputHandler}
                    />
                    <Input
                    id="zipCode"
                    element="input"
                    type="text"
                    label="Zip Code"
                    validators={[VALIDATOR_REQUIRE(), VALIDATOR_NUMBER(), VALIDATOR_MAXLENGTH(5)]}
                    errorText="Field required, only numbers. Max(5 digits)"
                    onInput={inputHandler}
                    />
                    <div className="checkList">
                        <div className="title">Hobbies:</div>
                        <div className="list-container">
                        {loadedHobbies && loadedHobbies.map((item, index) => (
                            <div key={index}>
                            <input id={item.id} value={item.name} type="checkbox" onChange={handleCheck}/>
                            <span>{item.name}</span>
                          </div>
                        ))}
                        </div>
                    </div>
                    <div className='center' style={{marginTop:"30px"}}>
                    <Button  type="submit" disabled={!formState.isValid}>Save </Button>
                    <Button  type="button" inverse onClick={resetHandler}>Clear </Button>
                    </div>
                </form>
                          
                {loadedStudent && <Student students={loadedStudent} />}
              
            </Card>
        </React.Fragment>
    )
}

export default NewStudent;