export class DBSCANConfig{
    public epsilon: number = 0;
    public minPoints: number = 1;

    constructor(eps:number, minPoints: number){
        this.epsilon = eps;
        this.minPoints = minPoints;
    }
}