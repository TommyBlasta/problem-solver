import React from 'react';
import styles from './Toolbar.module.css';

interface ToolbarProps {
  onAddProblem: () => void;
}

const ProblemToolbar: React.FC<ToolbarProps> = ({ onAddProblem }) => {
  return (
    <div className={styles.toolbar}>
      <button className={styles.addButton} onClick={onAddProblem}>
        Add Problem
      </button>
    </div>
  );
};

export default ProblemToolbar;
