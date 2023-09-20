package com.worldpath.community.DTO;

import lombok.Getter;
import lombok.Setter;

import java.util.Date;

@Getter
@Setter
public class CategoryDTO {
    /**
     * CategoryDTO is a DTO for Category.
     */
    private String id;
    private String title;
    private String description;
    private String background_colour;
    private Date updated_at;
    private Date created_at;
}
