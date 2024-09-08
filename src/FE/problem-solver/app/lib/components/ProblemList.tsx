import React from 'react';
import styles from './ProblemList.module.css';
import { ProblemDto } from '../interfaces/problemDto';

interface ProblemListProps {
  problems: ProblemDto[];
}

const ProblemList: React.FC<ProblemListProps> = ({ problems }) => {
  return (
    <div className={styles.problemContainer}>
      <h1 className={styles.heading}>List of Problems</h1>
      <ul className={styles.problemList}>
        {problems.map((problem) => (
          <li key={problem.id} className={styles.problemItem}>
            {problem.name}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ProblemList;
