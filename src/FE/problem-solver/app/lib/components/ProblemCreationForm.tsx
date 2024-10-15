import React, { useState } from 'react';
import { createProblem } from '../services/problemService';
import { ProblemCreationInputDto } from '../interfaces/problemCreationInputDto';
import styles from './ProblemCreationForm.module.css';

interface ProblemCreationFormProps {
}

const ProblemCreationForm: React.FC<ProblemCreationFormProps> = () => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [defaultInput, setDefaultInput] = useState('');

    const handleTitleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTitle(event.target.value);
    };

    const handleDescriptionChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        setDescription(event.target.value);
    };

    const handleDefaultInputChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        setDefaultInput(event.target.value);
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        // Create a new problem using the ProblemService
        const problem: ProblemCreationInputDto = {
            name: title,
            description: description,
            defaultInput: defaultInput,
            //TODO: Replace this with the actual category ID
            categoryId: 'c25f333f-75a4-4c63-9067-a20abab3f1b5',
        };

        createProblem(problem)
            .then((response) => {
                // Handle the response from the ProblemService
                console.log('Problem created successfully:', response);
            })
            .catch((error) => {
                // Handle any errors from the ProblemService
                console.error('Error creating problem:', error);
            });

        // Reset the form fields
        setTitle('');
        setDescription('');
        setDefaultInput('');
    };

    return (
        <form onSubmit={handleSubmit} className={styles.formContainer}>
            <h2 className={styles.formHeading}>Create a New Problem</h2>
            <div className={styles.formGroup}>
                <label htmlFor="title" className={styles.formLabel}>Title:</label>
                <input type="text" id="title" value={title} onChange={handleTitleChange} className={styles.formInput} />
            </div>
            <div className={styles.formGroup}>
                <label htmlFor="description" className={styles.formLabel}>Description:</label>
                <textarea id="description" value={description} onChange={handleDescriptionChange} className={styles.formTextarea} />
            </div>
            <div className={styles.formGroup}>
                <label htmlFor="defaultInput" className={styles.formLabel}>Default input:</label>
                <textarea id="defaultInput" value={defaultInput} onChange={handleDefaultInputChange} className={styles.formTextarea} />
            </div>
            <button type="submit" className={styles.submitButton}>Create Problem</button>
        </form>
    );
};

export default ProblemCreationForm;