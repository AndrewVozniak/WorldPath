package com.worldpath.community.model;

import lombok.Getter;
import org.springframework.data.mongodb.core.mapping.Document;

import java.util.Date;

@Getter
@Document(collection = "Topics")
public class Topic {
    private final String name;
    private final String description;
    private final String user_id;
    private final Date updated_at;
    private final Date created_at;

    public Topic(String title, String description, String user_id, Date updated_at, Date created_at){
        this.name = title;
        this.description = description;
        this.user_id = user_id;
        this.updated_at = updated_at;
        this.created_at = created_at;
    }

}
