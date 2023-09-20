package com.worldpath.community.model;

import lombok.Getter;
import lombok.Setter;

import javax.validation.constraints.NotEmpty;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;

@Getter
@Setter
@Document(collection = "Categories")
public class Category {
    @Id
    private String id;

    @NotEmpty(message = "Title is required.")
    private String title;

    @NotEmpty(message = "Description is required.")
    private String description;

    @Field("background_colour")
    private String backgroundColour;

    @Field("updated_at")
    private Date updatedAt;

    @Field("created_at")
    private Date createdAt;

    public void createCategoryDate() {
        this.createdAt = new Date();
        this.updatedAt = new Date();
    }

    public void updateCategory(String title, String description) {
        if (title != null && !title.isEmpty()) {
            this.title = title;
        }

        if (description != null && !description.isEmpty()) {
            this.description = description;
        }

        this.updatedAt = new Date();
    }
}
