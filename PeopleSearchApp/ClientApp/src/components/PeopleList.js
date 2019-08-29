import React, { Component } from 'react';
import { Redirect } from 'react-router-dom'
import { Person } from './Person';
import "./css/Image.css"

export class PeopleList extends Component {
    displayName = PeopleList.name

    constructor(props) {
        super(props);
        this.state = {
            people: [],
            searchString: "",
            loading: true,
            redirect: false
        };

        this.handleSearchSubmit = this.handleSearchSubmit.bind(this);
        this.handleSearchBoxChange = this.handleSearchBoxChange.bind(this);
        this.handleDeleteClick = this.handleDeleteClick.bind(this);

        this.getPeople(this.state.searchString);
    }

    //API Calls
    getPeople(searchString) {
        fetch('api/People?searchString=' + searchString, { method: 'GET' })
            .then(response => response.json())
            .then(data => {
                this.setState({ people: data, searchString: this.state.searchString, loading: false });
            });
    }

    deletePerson(id) {
        fetch('api/People/' + id, { method: 'DELETE' })
            .then(response => response.json());
    }

    //Event handlers
    handleSearchBoxChange(event) {
        let searchString = event.target.value;
        this.setState({ people: this.state.people, searchString: searchString, loading: false });
    }

    handleSearchSubmit(event) {
        event.preventDefault();
        let searchString = this.state.searchString;
        this.setState({ loading: true });
        this.getPeople(searchString);

        this.render();
    }

    handleEditClick(person) {
        this.setState({ redirect: true });
    }

    handleDeleteClick(person) {
        this.setState({ loading: true });
        this.deletePerson(person.id);
        this.getPeople('');

        this.render();
    }

    //Redirecting logic
    renderEditRedirect(person) {
        if (this.state.redirect) {
            return <Redirect
                to={'/Person/' + person.id }
                component={Person}
            />
        }
    }

    //image converter 
    convertImageFromBase64(binaryImage) {
        let image = new Image();
        image.src = 'data:image/jpeg;base64,' + binaryImage;
        return image;
    }

    createImageTag(imageBinary) {
        if (imageBinary !== undefined && imageBinary !== '' && imageBinary !== null) 
            return (<img class="img" id='personImage' src={`data:image/jpg;base64,${imageBinary}`} />);
    }

    //Rendering mechanisms 
    renderPeopleTable(state) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th hidden={true}>Id</th>
                        <th>Picture</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        state.people.map(person =>
                            <tr key={person.firstName}>
                                <td>{this.createImageTag(person.photograph)}</td>
                                <td>{person.firstName}</td>
                                <td>{person.lastName}</td>
                                <td>{person.age}</td>
                                <td>{person.streetAddress + ', ' + person.city + ', ' + person.state + ' ' + person.zipCode}</td>
                                <td>
                                    {this.renderEditRedirect(person)}
                                    <button onClick={() => this.handleEditClick(person)}>Edit</button>
                                </td>
                                <td><button onClick={() => this.handleDeleteClick(person)}>Delete</button></td>
                            </tr>
                        )
                    }
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderPeopleTable(this.state);

        return (
            <div>
                <h1>People</h1>
                <div class='form-group'>
                    <form onSubmit={this.handleSearchSubmit}>
                        <input type="text"
                            class='form-control'
                            onChange={this.handleSearchBoxChange}
                            placeholder="Search..."
                        />
                    </form>
                </div>
                {contents}
            </div>
        );
    }
}