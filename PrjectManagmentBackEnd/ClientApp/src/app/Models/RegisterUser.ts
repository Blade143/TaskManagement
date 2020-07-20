export class RegisterUser{
    constructor(
        public userId?: number,
        public login?: string,
        public password?: string,
        public userName?: string,
        public finished?: number,
        public rating?: number,
        public failed?: number
    ) {}
}