package com.worldpath.community.action;

import com.worldpath.community.DTO.TopicDTO;
import com.worldpath.community.action.interfaces.Action;
import com.worldpath.community.model.Topic;
import org.modelmapper.ModelMapper;

public class ConvertToTopicDTOAction implements Action<Topic, TopicDTO> {
    private final ModelMapper modelMapper;

    public ConvertToTopicDTOAction(ModelMapper modelMapper) {
        this.modelMapper = modelMapper;
    }

    @Override
    public TopicDTO execute(Topic topic) {
        TopicDTO topicDTO = modelMapper.map(topic, TopicDTO.class);
        topicDTO.setUser_id(topic.getUserId());
        topicDTO.setCreated_at(topic.getCreatedAt());
        topicDTO.setUpdated_at(topic.getUpdatedAt());
        return topicDTO;
    }
}
