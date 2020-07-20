export class Puttask{
    constructor(
        public taskId?: number,
        public userId?: number,
        public projectId?: number,
        public taskParentID?: number,
        public taskName?: string,
        public disription?: string,
        public status?: string,
        public createDate?: Date,
        public ratingPoints?: number,
        public estimateDate?: Date
    ) {}
}