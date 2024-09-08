import React from 'react';
import { getProblems } from '../lib/services/problemService';
import ProblemList from '../lib/components/ProblemList';

export async function Page() {
  let problems = await getProblems();
  return (
    <div>
      <ProblemList problems={problems} />
    </div>
  );
}

export default Page;
