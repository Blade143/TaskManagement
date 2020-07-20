export class Task{
    constructor(
        public taskId?: number,
        public userId?: number,
        public userName?: string,
        public projectId?: number,
        public projectTitle?: string,
        public taskParentID?: number,
        public taskName?: string,
        public taskDiscritpion?: string,
        public taskStatus?: string,
        public taskCreateDate?: Date,
        public ratingPoints?: number,
        public taskEstimeteDate?: Date
    ) {}
}