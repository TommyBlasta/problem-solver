import apiClient from './problemApi';
import { ProblemCreationInputDto} from '../interfaces/problemCreationInputDto';
import {ProblemDto} from '../interfaces/problemDto';
import {ProblemInputDto} from '../interfaces/problemInputDto';
import {ProblemResultDto} from '../interfaces/problemResultDto';

// Fetch all problems
export const getProblems = async (): Promise<ProblemDto[]> => {
  const response = await apiClient.get<ProblemDto[]>('/problems');
  return response.data;
};

// Create a new problem
export const createProblem = async (problem: ProblemCreationInputDto): Promise<string> => {
  const response = await apiClient.post<string>('/problems', problem);
  return response.data;
};

// Solve a problem
export const solveProblem = async (input: ProblemInputDto): Promise<ProblemResultDto> => {
  const response = await apiClient.post<ProblemResultDto>('/problems/solve', input);
  return response.data;
};
