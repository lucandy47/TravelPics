import {EntityTypeEnum} from '../../ui/enums/entity-type.enum'
export interface AvailableSearchItem{
    id: number;
    name: string;
    entityTypeId: EntityTypeEnum;
}