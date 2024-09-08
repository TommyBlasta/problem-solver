export interface ProblemCreationInputDto {
  name: string | null;
  description: string | null;
  defaultInput: string | null;
  categoryId: string;
}
