export class Project{
    constructor(
        public projectId?:number,
        public title?: string,
        public status?: string,
        public image?: string,
        public discription?: string,
        public role?: string
    ) {}
}