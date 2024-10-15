'use client';
import React, { useEffect, useState } from 'react';
import ProblemToolbar, { Selection } from '../lib/components/ProblemToolbar';

const Page = () => {
  const [problems, setProblems] = useState([]);

  return (
    <div>
      <ProblemToolbar activeSelection={ Selection.SolveProblem } />
      Solve problem page
    </div>
  );
};

export default Page;