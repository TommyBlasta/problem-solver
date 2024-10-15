'use client';
import React, { useEffect, useState } from 'react';
import ProblemCreationForm from '../lib/components/ProblemCreationForm';
import ProblemToolbar, { Selection } from '../lib/components/ProblemToolbar';

const Page = () => {
  const [problems, setProblems] = useState([]);

  return (
    <div>
      <ProblemToolbar activeSelection={ Selection.AddProblem } />
      <ProblemCreationForm />
    </div>
  );
};

export default Page;