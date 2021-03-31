import {PracticesAnexoVM} from "../view-models/PracticesAnexoVM";
import {PracticesAuthorsVM} from "../view-models/PracticesAuthorsVM";

export class PracticesVM{

    id : number;
    name : string; 
    description : string; 
    typesAiMlApplications : string; 
    organizationContext : string; 
    seKnowLedge : string; 
    contribuitionTypes : string; 
    link : string; 
    referencesDescribing : string; 
    idUser : number;

    anexosPractice : PracticesAnexoVM[];
    authorsPractice : PracticesAuthorsVM[];
}