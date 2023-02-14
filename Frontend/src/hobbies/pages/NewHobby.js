import React,{useState} from "react";
import { useHistory } from "react-router-dom";
import Card from "../../shared/components/UIElements/Card";
import Modal from "../../shared/components/UIElements/Modal";
import Input from "../../shared/components/FormElements/Input";
import Button from "../../shared/components/FormElements/Button";
import { useForm } from "../../shared/components/hooks/form-hook";
import ErrorModal from "../../shared/components/UIElements/ErrorModal";
import { useHttpClient } from "../../shared/components/hooks/http-hook";
import LoadingSpinner from "../../shared/components/UIElements/LoadingSpinner";
import { VALIDATOR_REQUIRE,VALIDATOR_MAXLENGTH } from "../../shared/components/utils/validator";


const NewHobby = () =>{
    const history = useHistory();
    const {isLoading, error,sendRequest,clearError} = useHttpClient();
    const [showError, setShowError] = useState(false);
    const [header, setHeader] = useState('');
    const cancelErrorHandler = () => {setShowError(false)};
    const [formState, inputHandler] =   useForm({
        name: {
            value: '',
            isValid: false
        }
        
    },false);
    
    
    const addHobbyHandler = async (event) =>{
        event.preventDefault();

        let newHobby = {name: formState.inputs.name.value};
        
        
        const responseData = await sendRequest(
            'https://localhost:7222/api/hobby',
            'POST',
            JSON.stringify(newHobby),
            {'Content-Type': 'application/json'}); 

        if(responseData.success){
            newHobby = responseData.student;
            history.push("/");
        }
        else{
            setHeader(responseData.xerror);
            setShowError(true);
        }
    };

    return(
        <React.Fragment>
            <Modal show={showError} 
            onCancel={cancelErrorHandler} 
            header="Error message"
            children={header}
            contentClass="place-item__modal-content" 
            footerClass="place-item__modal-actions" 
            footer={<Button onClick={cancelErrorHandler}>CLOSE</Button>}>
            </Modal>
            <ErrorModal error={error} onClear={clearError} />
            <Card className="authentication">
                <h2>Students</h2>
                <hr/>
                <form onSubmit={addHobbyHandler} className="place-form">
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
                    <div className='center' style={{marginTop:"30px"}}></div>
                    <Button  type="submit" disabled={!formState.isValid}>Save </Button>
                </form>
            </Card>
        </React.Fragment>
    )
}

export default NewHobby;