export class TriviaService {
    public getSimpleMathProblem(): { num1: number; num2: number; sum: number }  {
        const num1 = Math.floor(Math.random() * 100);
        const num2 = Math.floor(Math.random() * 100);
        const sum = num1 + num2;

        return { num1, num2, sum };
    }
}
