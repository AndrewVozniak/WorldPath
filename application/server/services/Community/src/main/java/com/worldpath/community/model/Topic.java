package com.worldpath.community.model;

import lombok.Getter;
import lombok.Setter;
import org.springframework.data.mongodb.core.mapping.Document;
import javax.validation.constraints.NotEmpty;
import org.springframework.data.annotation.Id;

import java.util.Date;

@Getter
@Setter
@Document(collection = "Topics")
public class Topic {
    @Id
    private String id;

    @NotEmpty(message = "Title is required.")
    private String title;

    @NotEmpty(message = "Description is required.")
    private String description;

    @NotEmpty(message = "User ID is required.")
    private String user_id;

    private Date updated_at = new Date();
    private Date created_at = new Date();
}
