import { User } from "./user";

export class UserParams {
    gender:"male" | "famale";
    minAge = 18;
    max = 150;
    pageNumber = 1;
    pageSize = 5;
    orderBy = "lastActive";
 
    constructor({gender}:User) {
    this.gender = gender==="male"? "famale":"male";        
    }
}
