import React, { Component } from 'react';
import { Redirect } from 'react-router-dom'

export class Person extends Component {
    constructor(props) {
        super(props);

        this.state = {
            method: 'POST',
            endpoint: 'api/People',
            formControls: {
                id: '',
                firstName: '',
                lastName: '',
                age: '',
                streetAddress: '',
                city: '',
                state: '',
                zipCode: '',
                photograph: ''
            },
            redirect: false,
            reader: new FileReader()
        }

        if (props.match.params.id !== undefined && props.pathName !== '') {
            fetch('api/People/' + props.match.params.id, { method: 'GET' })
                .then(response => response.json())
                .then(data =>
                    this.setState({
                        method: 'PUT',
                        endpoint: 'api/People/' + data.id,
                        formControls: {
                            id: data.id,
                            firstName: data.firstName,
                            lastName: data.lastName,
                            age: data.age,
                            streetAddress: data.streetAddress,
                            city: data.city,
                            state: data.state,
                            zipCode: data.zipCode,
                            photograph: data.photoGraph
                        },
                        redirect: false,
                        reader: new FileReader()
                    })
            );
        }

        let self = this;
        this.state.reader.onloadend = function (event) {
            self.setState({ photograph: event.target.result.split(',')[1] });
        }

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleImageAdd = this.handleImageAdd.bind(this);
    }

    //Event handlers
    handleChange(event) {
        let name = event.target.name;
        let value = event.target.value;
        this.state.formControls[name] = value;
    }

    handleImageAdd(event) {
        let elem = document.getElementById("personPhoto");
        let file = elem.files[0];
        this.state.reader.readAsDataURL(file);
    }

    handleSubmit(event) {
        event.preventDefault();

        let body = {
            FirstName: this.state.formControls.firstName,
            LastName: this.state.formControls.lastName,
            Age: this.state.formControls.age,
            StreetAddress: this.state.formControls.streetAddress,
            City: this.state.formControls.city,
            State: this.state.formControls.state,
            ZipCode: this.state.formControls.zipCode,
            Photograph: this.state.formControls.photograph
        };

        if (this.state.method === 'PUT') {
            body.Id = this.state.formControls.id;
        }
        
        let requestParams = {
            method: this.state.method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        }

        fetch(this.state.endpoint, requestParams);

        this.setState({ redirect: true });
    }

    //Redirect logic
    renderRedirect = () => {
        if (this.state.redirect) {
            return <Redirect to='/' />
        }
    }

    //Rendering mechanisms
    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                {this.renderRedirect()}
                <div class='form-group'>
                    <label>First Name:</label>
                    <input type="text"
                        class="form-control"
                        name='firstName'
                        placeholder={this.state.formControls.firstName}
                        onChange={this.handleChange}
                        required
                    />
                </div>
                <div class='form-group'>
                    <label>Last Name:</label>
                    <input type="text"
                        class="form-control"
                        name='lastName'
                        placeholder={this.state.formControls.lastName}
                        onChange={this.handleChange}
                        required
                    />
                </div>
                <div class='form-group'>
                    <label>Age:</label>
                    <input type="number"
                        class="form-control"
                        name='age'
                        placeholder={this.state.formControls.age}
                        onChange={this.handleChange}
                        required
                    />
                </div>
                <div class='form-group'>
                    <label>Street Address:</label>
                    <input type="text"
                        class="form-control"
                        name='streetAddress'
                        placeholder={this.state.formControls.streetAddress}
                        onChange={this.handleChange}
                        required
                    />
                </div>
                <div class='form-group'>
                    <label>City:</label>
                    <input type="text"
                        class="form-control"
                        name='city'
                        placeholder={this.state.formControls.city}
                        onChange={this.handleChange}
                        required
                    />
                </div>
                <div class='form-group'>
                    <label>State:</label>
                    <input type="text"
                        class="form-control"
                        name='state'
                        placeholder={this.state.formControls.state}
                        onChange={this.handleChange}
                        required
                    />
                </div>
                <div class='form-group'>
                    <label>Zip Code:</label>
                    <input type="number"
                        class="form-control"
                        name='zipCode'
                        placeholder={this.state.formControls.zipCode}
                        onChange={this.handleChange}
                        required
                    />
                </div>
                <div class='form-group'>
                    <label>Photograph:</label>
                    <input type="file"
                        class="form-control"
                        accept='.jpeg,.png,.jpg'
                        id='personPhoto'
                        name='photograph'
                        onChange={this.handleImageAdd}
                    />
                </div>
                <input type="submit" id='submitPerson' value="Submit" />
            </form>
        );
    }
}