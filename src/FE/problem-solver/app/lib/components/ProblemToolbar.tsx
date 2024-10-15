import React from 'react';
import styles from './ProblemToolbar.module.css';
import Link from 'next/link'

interface ToolbarProps {
  onAddProblem?: () => void;
  onSolveProblem?: () => void;
  onProblemList?: () => void;
  activeSelection: Selection;
}

export enum Selection {
  AddProblem,
  ProblemList,
  SolveProblem
}

const ProblemToolbar: React.FC<ToolbarProps> = ({ onAddProblem, onProblemList, onSolveProblem, activeSelection }) => {
  return (
    <div className={styles.toolbar}>
        <Link href="/create-problem">
          <button className={`${styles.buttonBase} ${styles.addButton}`} onClick={onAddProblem}>
          Add Problem
          </button> 
        </Link>

        <Link href="/solve-problem">
          <button className={`${styles.buttonBase} ${styles.solveButton}`} onClick={onSolveProblem}>
          Solve Problem
          </button> 
        </Link>

        <Link href="/problem-solver">
          <button className={`${styles.buttonBase} ${styles.listButton}`} onClick={onProblemList}>
          Problem List
          </button> 
        </Link>
    </div>
  );
};

export default ProblemToolbar;
