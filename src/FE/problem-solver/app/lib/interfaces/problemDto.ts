export interface ProblemDto {
    id: string;
    name: string | null;
    description: string | null;
    defaultInput: string | null;
    categoryId: string;
  }