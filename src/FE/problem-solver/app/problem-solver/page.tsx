'use client';
import React, { useEffect, useState } from 'react';
import { getProblems } from '../lib/services/problemService';
import ProblemList from '../lib/components/ProblemList';
import ProblemToolbar, { Selection } from '../lib/components/ProblemToolbar';

const Page = () => {
  const [problems, setProblems] = useState([]);

  useEffect(() => {
    const fetchProblems = async () => {
      const problemsData = await getProblems();
      setProblems(problemsData);
    };

    fetchProblems();
  }, []);

  const handleAddProblem = () => {
    console.log("Add Problem button clicked!");
  };

  return (
    <div>
      <ProblemToolbar activeSelection={ Selection.ProblemList } />
      <ProblemList problems={problems} />
    </div>
  );
};

export default Page;
