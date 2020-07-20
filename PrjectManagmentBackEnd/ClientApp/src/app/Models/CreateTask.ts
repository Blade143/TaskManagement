export class CreateTask{
    constructor(
        public taskId?: number,
        public userId?: number,
        public projectId?: number,
        public parentTaskId?: number,
        public taskName?: string,
        public disription?: string,
        public status?: string,
        public ratingPoints?: number,
        public createDate?: Date,
        public estimateDate?: Date
    ) {}
}