export interface ICharacterCreate {
    name: string;
    guildId: number | null;
    cityId: number;
    raceId: number;
    ecoId: number | null;
    userId: number;
    image: File;
}

